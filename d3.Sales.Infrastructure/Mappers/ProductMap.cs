using d3.Sales.Domain.Products;
using d3.Sales.Infrastructure.Mappers.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Mappers
{
    public class ProductMap : AggregateRootMap<Product>
    {
        public ProductMap()
        {
            Map(x => x.Price).Not.Nullable()
                    .CustomType<MoneyUserType>()
                    .Columns.Clear()
                    .Columns.Add("Product_Value");
            Map(x => x.Name).Not.Nullable();
        }
    }
}
