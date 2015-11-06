using d3.Sales.Domain.Vouchers;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private ISession _session;

        public VoucherRepository(ISession session)
        {
            _session = session;
        }

        public Voucher GetVoucher(Guid id)
        {
            return _session.Get<Voucher>(id);
        }

        public Voucher GetVaucherByCode(string code)
        {
            var voucherCode = new VoucherCode(code);
            return _session.Query<Voucher>().SingleOrDefault(v => v.Code == voucherCode); 
        }

        public void Add(Voucher voucher)
        {
            _session.Save(voucher);
        }
    }
}
