using d3.Framework.Domain;
using d3.Framework.Events;
using d3.Sales.Domain.Products;
using d3.Sales.Domain.Products.Events;
using d3.Sales.Domain.Products.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Domain.Products
{
    public class Product : AggregateRoot
    {
        public Product(string name, Money price)
        {
            if (String.IsNullOrEmpty(name))
                throw new InvalidProductNameException();

            Id = Guid.NewGuid();
            Name = name;
            Price = price;

            DomainEvents.Raise(new ProductCreated(Id, Name, Price));
        }

        protected Product() { }

        public virtual string Name { get; protected set; }
        public virtual Money Price { get; protected set; }

    }
}
