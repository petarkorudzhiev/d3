using d3.Sales.Domain.Purchases;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private ISession _session;

        public PurchaseRepository(ISession session)
        {
            _session = session;
        }

        public void Add(Purchase purchase)
        {
            _session.Save(purchase);
        }
    }
}
