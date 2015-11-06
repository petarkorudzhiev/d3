using d3.Sales.Application.CommandModels.Product;
using d3.Sales.Application.QueryModels.Product;
using d3.Sales.Domain.Products;
using d3.Sales.Domain.Products.Exceptions;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Application.AppServices
{
    public class ProductAppService : IProductAppService
    {
        private ISession _session;
        private IProductRepository _productRepository;

        public ProductAppService(ISession session, IProductRepository productRepository)
        {
            _session = session;
            _productRepository = productRepository;
        }

        public ProductsViewModel GetIndexData()
        {
            var products = _session.Query<Product>().ToList();

            var model = new ProductsViewModel();

            foreach(var product in products)
            {
                model.Products.Add(new ProductViewModel() { Id = product.Id, Name = product.Name, Price = product.Price.Value });
            }

            return model;
        }


        public CreateProductViewModel GetCreateData()
        {
            return new CreateProductViewModel();
        }
        public CreateProductViewModel GetCreateData(CreateProductViewModel model)
        {
            return new CreateProductViewModel()
            {
                Name = model.Name,
                Price = model.Price
            };
        }
        public void Create(CreateProductViewModel model)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                try
                {
                    var product = new Product(model.Name, new Money(model.Price));
                    _productRepository.Add(product);
                }
                catch (MoneyWithMoreThanTwoDecimalPlacesException m1)
                {
                    throw new ApplicationException(String.Format("Invalid product price: {0}", m1.Value));
                }
                catch (MoneyCannotBeNegativeException m2)
                {
                    throw new ApplicationException(String.Format("Product price can't be negative: {0}", m2.Value));
                }
                catch (InvalidProductNameException p1)
                {
                    throw new ApplicationException(String.Format("Invalid product name"));
                }

                transaction.Commit();
            }
        }
    }
}
