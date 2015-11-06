using d3.Sales.Domain.Customers;
using d3.Sales.Infrastructure.Mappers.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Mappers
{
    public class CustomerMap : AggregateRootMap<Customer>
    {
        public CustomerMap()
        {
            Map(p => p.FirstName).Not.Nullable();
            Map(p => p.LastName).Not.Nullable();
            Map(p => p.Phone).Nullable().CustomType<PhoneUserType>();
            Map(p => p.Email).Nullable().CustomType<EmailUserType>();
            Component(p => p.DeliveryAddress);
        }
    }
}
