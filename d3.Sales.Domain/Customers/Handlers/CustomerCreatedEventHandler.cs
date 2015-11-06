using d3.Sales.Domain.Carts;
using d3.Framework.Events;
using d3.Sales.Domain.Customers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Customers.Handlers
{
    public class CustomerCreatedEventHandler : IEventHandler<CustomerCreated>
    {
        private readonly ICartRepository _repository;

        public CustomerCreatedEventHandler(ICartRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CustomerCreated message)
        {
            var cart = new Cart(message.Id);

            _repository.Add(cart);
        }
    }
}
