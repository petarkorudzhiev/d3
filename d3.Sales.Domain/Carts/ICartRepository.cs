using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Carts
{
    public interface ICartRepository
    {
        void Add(Cart cart);
        Cart GetCartByCustomerId(Guid customerId);
    }
}
