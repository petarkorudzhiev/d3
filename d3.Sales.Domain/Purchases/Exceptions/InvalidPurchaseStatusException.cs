using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Purchases.Exceptions
{
    public class InvalidPurchaseStatusException : Exception
    {
        private PurchaseStatus _status;

        public InvalidPurchaseStatusException(PurchaseStatus status)
        {
            _status = status;
        }

        public PurchaseStatus Status
        {
            get { return _status; }
        }
    }
}
