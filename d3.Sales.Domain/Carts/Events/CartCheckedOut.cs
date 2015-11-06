using d3.Framework.Events;
using d3.Sales.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Carts.Events
{
    public class CartCheckedOut : IEvent
    {
        public CartCheckedOut(Guid customerId, Guid? voucherId, DeliveryAddress address, Phone phone)
        {
            CustomerId = customerId;
            VoucherId = voucherId;
            Address = address;
            Phone = phone;
        }

        public Guid CustomerId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public DeliveryAddress Address { get; private set; }
        public Phone Phone { get; private set; }
    }
}
