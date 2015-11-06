using d3.Sales.Domain.Customers;
using d3.Sales.Domain.Products;
using d3.Sales.Domain.Purchases;
using d3.Framework.Domain;
using d3.Sales.Domain.Carts;
using d3.Sales.Domain.Carts.Events;
using d3.Sales.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d3.Sales.Domain.Products.Exceptions;
using d3.Sales.Domain.Carts.Exceptions;
using d3.Sales.Domain.Vouchers.Exceptions;
using d3.Framework.Events;

namespace d3.Sales.Domain.Carts
{
    public class Cart : AggregateRoot
    {
        #region Fields

        private IList<CartItem> _items;

        #endregion

        #region Constructors and Factories

        public Cart(Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentNullException("customerId");

            Id = Guid.NewGuid();
            CustomerId = customerId;
            VoucherAmount = null;
            VoucherId = null;
            _items = new List<CartItem>();

            DomainEvents.Raise(new CartCreated(Id, CustomerId));
        }
        protected Cart() { }

        #endregion

        #region Public State

        public virtual Guid CustomerId { get; protected set; }
        public virtual IReadOnlyCollection<CartItem> Items 
        {
            get { return _items.ToList().AsReadOnly(); } 
        }
        public virtual Money VoucherAmount { get; protected set; }
        public virtual Guid? VoucherId { get; protected set; }

        #endregion

        #region Public Interface

        public virtual Money GetTotalPrice()
        {
            Money price = Money.Empty;

            foreach (var item in Items)
            {
                price += item.Price * item.Quantity;
            }

            if (VoucherAmount != null && price > VoucherAmount)
            {
                price = price - VoucherAmount;
            }
            return price;
        }
        public virtual void AddProduct(Product product, Quantity quantity)
        {
            if (product == null)
                throw new InvalidProductInstanceException();

            if (quantity == null)
                throw new InvalidQuantityInstanceException();

            _items.Add(new CartItem(product.Id, quantity, product.Price));

            DomainEvents.Raise(new ProductAddedToCart(CustomerId, product.Id, quantity, product.Price));
        }
        public virtual void RemoveProduct(Product product)
        {
            if (product == null)
                throw new InvalidProductInstanceException();

            var item = _items.SingleOrDefault(p => p.ProductId == product.Id);
            if (item == null)
                throw new ProductNotFoundInCartException();

            _items.Remove(item);

            DomainEvents.Raise(new ProductRemovedFromCart(CustomerId, product.Id));
        }
        public virtual void ApplyVoucher(Voucher voucher)
        {
            if (voucher == null)
                throw new InvalidVoucherInstanceException();

            VoucherId = voucher.Id;
            VoucherAmount = voucher.Amount;

            DomainEvents.Raise(new VoucherAppliedToCart(CustomerId, voucher.Id, voucher.Amount));
        }
        public virtual Purchase Checkout(DeliveryAddress address, Phone phone)
        {
            if (_items.Count == 0)
                throw new CartEmptyException();

            var purchase = new Purchase(this);

            DomainEvents.Raise(new CartCheckedOut(CustomerId, VoucherId, address, phone));

            _items.Clear();
            VoucherId = null;
            VoucherAmount = null;

            return purchase;
        }

        #endregion
    }
}
