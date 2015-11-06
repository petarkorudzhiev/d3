using d3.Sales.Domain.Vouchers;
using d3.Sales.Infrastructure.Mappers.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Mappers
{
    public class VoucherMap : AggregateRootMap<Voucher>
    {
        public VoucherMap()
        {
            Map(p => p.CustomerId).Not.Nullable();
            Map(p => p.ExpirationDate).Not.Nullable();
            Map(p => p.Status).Not.Nullable();
            Map(p => p.Amount).Not.Nullable().CustomType<MoneyUserType>();
            Map(p => p.Code).Not.Nullable().CustomType<VoucherCodeUserType>();
        }
    }
}
