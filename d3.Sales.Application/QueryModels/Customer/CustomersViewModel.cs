using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Application.QueryModels.Customer
{
    public class CustomersViewModel
    {
        public CustomersViewModel()
        {
            Customers = new List<CustomerViewModel>();
        }

        public List<CustomerViewModel> Customers { get; private set; }
    }

    public class CustomerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
