using d3.Framework.Events;
using d3.Sales.Domain.Carts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Vouchers.Handlers
{
    public class CartCheckedOutEventHandler : IEventHandler<CartCheckedOut>
    {
        private IVoucherRepository _voucherRepository;

        public CartCheckedOutEventHandler(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public void Handle(CartCheckedOut message)
        {
            if (message.VoucherId.HasValue)
            {
                var voucher = _voucherRepository.GetVoucher(message.VoucherId.Value);

                voucher.MarkAsUsed();
            }
        }
    }
}
