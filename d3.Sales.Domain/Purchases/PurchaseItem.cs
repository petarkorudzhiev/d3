using d3.Sales.Domain.Products;
using d3.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Purchases
{
    public class PurchaseItem : Entity, ICanFindMyAggregateRoot
    {
        public PurchaseItem(Purchase purchase, Guid productId, Money price, Quantity quantity)
        {
            Purchase = purchase;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }
        protected PurchaseItem() { }

        public virtual Guid ProductId { get; protected set; }
        public virtual Money Price { get; protected set; }
        public virtual Quantity Quantity { get; protected set; }
        public virtual Purchase Purchase { get; protected set; }
        public virtual AggregateRoot MyRoot
        {
            get { return Purchase; }
        }
    }
}
