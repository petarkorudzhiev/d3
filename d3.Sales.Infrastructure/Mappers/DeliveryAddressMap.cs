using d3.Sales.Domain.Customers;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Mappers
{
    public class DeliveryAddressMap : ComponentMap<DeliveryAddress>
    {
        public DeliveryAddressMap()
        {
            Map(p => p.Address).Nullable();
            Map(p => p.City).Nullable();
        }
    }
}
