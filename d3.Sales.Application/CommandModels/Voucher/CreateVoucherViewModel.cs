using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace d3.Sales.Application.CommandModels.Voucher
{
    public class CreateVoucherViewModel : BaseViewModel
    {
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}