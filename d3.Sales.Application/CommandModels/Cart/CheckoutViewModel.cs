using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace d3.Sales.Application.CommandModels.Cart
{
    public class CheckoutViewModel : BaseViewModel
    {
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; }
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string Phone { get; set; }
    }
}