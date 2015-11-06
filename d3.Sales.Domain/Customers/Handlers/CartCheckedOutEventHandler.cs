using d3.Framework.Events;
using d3.Sales.Domain.Carts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Customers.Handlers
{
    public class CartCheckedOutEventHandler : IEventHandler<CartCheckedOut>
    {
        private readonly ICustomerRepository _repository;

        public CartCheckedOutEventHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CartCheckedOut message)
        {
            var customer = _repository.GetCustomer(message.CustomerId);

            customer.SetDeliveryAddress(message.Address);
            customer.SetPhone(message.Phone);          
        }
    }
}
