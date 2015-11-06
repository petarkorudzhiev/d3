using d3.Sales.Application.CommandModels.Customer;
using d3.Sales.Application.QueryModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Application.AppServices
{
    public interface ICustomerAppService
    {
        CustomersViewModel GetIndexData();
        void Create(CreateCustomerViewModel model);
    }
}
