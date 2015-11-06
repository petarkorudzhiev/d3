using d3.Sales.Domain.Purchases;
using d3.Sales.Infrastructure.Mappers.UserTypes;
using FluentNHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Mappers
{
    public class PurchaseMap : AggregateRootMap<Purchase>
    {
        public PurchaseMap()
        {
            Map(p => p.CustomerId).Not.Nullable();
            Map(p => p.TotalPrice).Not.Nullable()
                    .CustomType<MoneyUserType>()
                    .Columns.Clear()
                    .Columns.Add("Purchase_Value");
            Map(p => p.VoucherId).Nullable();
            Map(p => p.Status).Not.Nullable();
            HasMany<PurchaseItem>(Reveal.Member<Purchase>("_items"))
                .Cascade
                .SaveUpdate()
                .Inverse().KeyColumn("PurchaseId")
                .Not.LazyLoad(); // When loading aggregate it is required all items in aggregate graph to be loaded together so we don't want to use lazy loading. 
        }
    }
}
