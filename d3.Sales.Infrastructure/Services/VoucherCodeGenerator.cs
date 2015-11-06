using d3.Sales.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Services
{
    public class VoucherCodeGenerator : IVoucherCodeGenerator
    {
        public VoucherCode GenerateCode()
        {
            return new VoucherCode("ABCD");
        }
    }
}
