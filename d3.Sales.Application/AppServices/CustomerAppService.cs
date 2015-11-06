using d3.Sales.Application.CommandModels.Customer;
using d3.Sales.Application.QueryModels.Customer;
using d3.Sales.Domain.Customers;
using d3.Sales.Domain.Customers.Exceptions;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Application.AppServices
{
    public class CustomerAppService : ICustomerAppService
    {
        private ISession _session;
        private ICustomerRepository _customerRepository;

        public CustomerAppService(ISession session, ICustomerRepository customerRepository)
        {
            _session = session;
            _customerRepository = customerRepository;
        }

        public CustomersViewModel GetIndexData()
        {
            var customers = _session.Query<Customer>().ToList();
            var model = new CustomersViewModel();

            foreach(var customer in customers)
            {
                model.Customers.Add(new CustomerViewModel() { FirstName = customer.FirstName, LastName = customer.LastName });
            }

            return model;
        }

        public void Create(CreateCustomerViewModel model)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                try
                { 
                    var customer = new Customer(model.FirstName, model.LastName);
                    _customerRepository.Add(customer);
                }
                catch (InvalidCustomerFirstNameException)
                {
                    throw new ApplicationException("Invalid first name");
                }
                catch(InvalidCustomerLastNameException)
                {
                    throw new ApplicationException("Invalid last name");
                }
                transaction.Commit();
            }
        }
    }
}
