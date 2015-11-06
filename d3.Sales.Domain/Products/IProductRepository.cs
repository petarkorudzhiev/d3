using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Products
{
    public interface IProductRepository
    {
        Product GetProduct(Guid id);
        void Add(Product product);
    }
}
