using d3.Sales.Domain.Carts;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private ISession _session;

        public CartRepository(ISession session)
        {
            _session = session;
        }

        public void Add(Cart cart)
        {
            _session.Save(cart);          
        }

        public Cart GetCartByCustomerId(Guid customerId)
        {
            return _session.Query<Cart>().Single(c => c.CustomerId == customerId);
        }
    }
}
