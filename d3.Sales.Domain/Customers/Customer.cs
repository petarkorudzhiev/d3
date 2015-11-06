using d3.Framework.Domain;
using d3.Framework.Events;
using d3.Sales.Domain.Customers;
using d3.Sales.Domain.Customers.Events;
using d3.Sales.Domain.Customers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Customers
{
    public class Customer : AggregateRoot
    {
        public Customer(string firstName, string lastName)
        {
            if (String.IsNullOrEmpty(firstName))
                throw new InvalidCustomerFirstNameException();

            if (String.IsNullOrEmpty(lastName))
                throw new InvalidCustomerLastNameException();

            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;

            DomainEvents.Raise(new CustomerCreated(Id, FirstName, LastName));
        }
        protected Customer() { }

        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual Email Email { get; protected set; }
        public virtual Phone Phone { get; protected set; }
        public virtual DeliveryAddress DeliveryAddress { get; protected set; }

        public virtual void SetDeliveryAddress(DeliveryAddress address)
        {
            DeliveryAddress = address;

            DomainEvents.Raise(new CustomerDeliveryAddressChanged(Id, address));
        }
        public virtual void SetEmail(Email email)
        {
            Email = email;

            DomainEvents.Raise(new CustomerEmailChanged(Id, email));
        }
        public virtual void SetPhone(Phone phone)
        {
            Phone = phone;

            DomainEvents.Raise(new CustomerPhoneChanged(Id, phone));
        }
    }
}
