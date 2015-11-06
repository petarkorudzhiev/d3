using d3.Sales.Domain.Products;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ISession _session;

        public ProductRepository(ISession session)
        {
            _session = session;
        }

        public Product GetProduct(Guid id)
        {
            return _session.Get<Product>(id);
        }
        public void Add(Product product)
        {
            _session.Save(product);
        }
    }
}
