using d3.Sales.Domain.Products;
using d3.Framework.Domain;
using d3.Sales.Domain.Vouchers;
using d3.Sales.Domain.Vouchers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d3.Sales.Domain.Vouchers.Exceptions;
using d3.Sales.Domain.Customers.Exceptions;

namespace d3.Sales.Domain.Vouchers
{
    public class Voucher : AggregateRoot
    {
        public static Voucher CreateVoucher(Guid customerId, Money amount, IVoucherCodeGenerator codeGenerator)
        {
            // Validate domain invariants
            if (customerId == Guid.Empty)
                throw new InvalidCustomerIdException();

            return new Voucher()
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                Code = codeGenerator.GenerateCode(),
                Status = VoucherStatus.Created,
                ExpirationDate = DateTime.UtcNow.AddDays(14),
                Amount = amount
            };
        }
        protected Voucher() { }

        public virtual Guid CustomerId { get; protected set; }
        public virtual VoucherCode Code { get; protected set; }
        public virtual VoucherStatus Status { get; protected set; }
        public virtual DateTime ExpirationDate { get; protected set; }
        public virtual Money Amount { get; protected set; }

        public virtual bool IsValid()
        {
            return Status == VoucherStatus.Created && ExpirationDate > DateTime.UtcNow;
        }
        public virtual void MarkAsUsed()
        {
            if (!IsValid())
                throw new InvalidVoucherException();

            Status = VoucherStatus.Used;
        }
    }
}
