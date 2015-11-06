using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace d3.Sales.Application.CommandModels.Product
{
    public class CreateProductViewModel : BaseViewModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}