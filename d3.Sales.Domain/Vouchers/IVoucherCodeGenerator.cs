﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Vouchers
{
    public interface IVoucherCodeGenerator
    {
        VoucherCode GenerateCode();
    }
}
