﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Customers
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(Guid id);
        void Add(Customer customer);
    }
}
