using d3.Sales.Domain.Purchases;
using d3.Sales.Infrastructure.Mappers.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Mappers
{
    public class PurchaseItemMap : EntityMap<PurchaseItem>
    {
        public PurchaseItemMap()
        {
            Map(p => p.ProductId).Not.Nullable();
            Map(p => p.Quantity).Not.Nullable().CustomType<QuantityUserType>();
            References(r => r.Purchase).Column("PurchaseId").Not.Nullable();
        }
    }
}
