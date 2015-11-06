using d3.Sales.Domain.Carts;
using d3.Sales.Domain.Products;
using d3.Framework.Domain;
using d3.Sales.Domain.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d3.Sales.Domain.Purchases.Events;
using d3.Framework.Events;
using d3.Sales.Domain.Purchases.Exceptions;

namespace d3.Sales.Domain.Purchases
{
    public class Purchase : AggregateRoot
    {
        private IList<PurchaseItem> _items;

        public Purchase(Cart cart)
        {
            // TODO: Validate domain invariants

            CustomerId = cart.CustomerId;
            VoucherId = cart.VoucherId;
            TotalPrice = cart.GetTotalPrice();
            Status = PurchaseStatus.Created;
            _items = new List<PurchaseItem>();

            foreach (var item in cart.Items)
            {
                _items.Add(new PurchaseItem(this, item.ProductId, item.Price, item.Quantity));
            }

            DomainEvents.Raise(new PurchaseCreated(CustomerId, VoucherId, TotalPrice, _items.ToList()));
        }
        protected Purchase() { }

        public virtual Guid CustomerId { get; protected set; }
        public virtual Money TotalPrice { get; protected set; }
        public virtual Guid? VoucherId { get; protected set; }
        public virtual PurchaseStatus Status { get; protected set; }
        public virtual IReadOnlyCollection<PurchaseItem> Items { get { return _items.ToList().AsReadOnly(); } }

        public virtual void Paid()
        {
            if (Status == PurchaseStatus.Created)
                Status = PurchaseStatus.Paid;
            else
                throw new InvalidPurchaseStatusException(Status);
        }
        public virtual void ReadyForDelivery()
        {
            if (Status == PurchaseStatus.Paid)
                Status = PurchaseStatus.ReadyForDelivery;
            else
                throw new InvalidPurchaseStatusException(Status);
        }
        public virtual void OnTheWay()
        {
            if (Status == PurchaseStatus.ReadyForDelivery)
                Status = PurchaseStatus.OnTheWay;
            else
                throw new InvalidPurchaseStatusException(Status);
        }
        public virtual void Delivered()
        {
            if (Status == PurchaseStatus.OnTheWay)
                Status = PurchaseStatus.Delivered;
            else
                throw new InvalidPurchaseStatusException(Status);
        }
    }
}
