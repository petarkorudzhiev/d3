using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace d3.Sales.Application.CommandModels.Cart
{
    public class ApplyVoucherViewModel : BaseViewModel
    {
        public Guid CustomerId { get; set; }
        public string VoucherCode { get; set; }
    }
}