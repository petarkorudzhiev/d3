using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace d3.Sales.Application.CommandModels.Cart
{
    public class AddProductViewModel : BaseViewModel
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}