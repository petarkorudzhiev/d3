using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Vouchers
{
    public interface IVoucherRepository
    {
        void Add(Voucher voucher);
        Voucher GetVoucher(Guid id);
        Voucher GetVaucherByCode(string code);
    }
}
