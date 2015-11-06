using d3.Sales.Domain.Customers;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private ISession _session;

        public CustomerRepository(ISession session)
        {
            _session = session;
        }

        public Customer GetCustomer(Guid id)
        {
            return _session.Get<Customer>(id);
        }

        public void Add(Customer customer)
        {
            _session.Save(customer);
        }
    }
}
