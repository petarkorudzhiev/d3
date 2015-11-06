using d3.Sales.Domain.Carts;
using d3.Sales.Infrastructure.Mappers.UserTypes;
using FluentNHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Mappers
{
    public class CartMap : AggregateRootMap<Cart>
    {
        public CartMap()
        {
            Map(p => p.CustomerId).Not.Nullable();
            Map(p => p.VoucherId).Nullable();
            Map(p => p.VoucherAmount).Nullable().CustomType<MoneyUserType>();

            HasMany<CartItem>(Reveal.Member<Cart>("_items"))
              .Table("CartItems")
              .KeyColumn("CartId")
              .Component(comp =>
              {
                  comp.Map(x => x.Price)
                      .Not.Nullable()
                      .CustomType<MoneyUserType>()
                      .Columns.Clear()
                      .Columns.Add("Item_Value");
                  comp.Map(x => x.ProductId).Not.Nullable();
                  comp.Map(x => x.Quantity).Not.Nullable().CustomType<QuantityUserType>();
              })
              .Not.LazyLoad(); // When loading aggregate it is required all items in aggregate graph to be loaded together so we don't want to use lazy loading. 
        }
    }
}
