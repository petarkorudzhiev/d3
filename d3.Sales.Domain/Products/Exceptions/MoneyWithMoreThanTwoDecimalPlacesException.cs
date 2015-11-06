using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Products.Exceptions
{
    public class MoneyWithMoreThanTwoDecimalPlacesException : Exception
    {
        private decimal _value;

        public MoneyWithMoreThanTwoDecimalPlacesException(decimal value)
        {
            _value = value;
        }

        public decimal Value
        {
            get { return _value; }
        }
    }
}
