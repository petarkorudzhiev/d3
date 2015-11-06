using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using d3.WebUI.Config;
using NHibernate;
using NHibernate.Context;
using d3.Framework.Events;
using d3.Framework.Bus;
using d3.WebUI.Controllers;
using System.Web.Mvc;
using d3.Sales.Domain.Customers;
using System.Collections.Generic;
using d3.Sales.Domain.Products;
using d3.Sales.Application.CommandModels.Cart;
using Castle.Windsor;

namespace d3.WebUI.Tests
{
    [TestClass]
    public class CartUnitTests
    {
        private IWindsorContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = Bootstrapper.InitializeTests();
            DomainEvents.Bus = _container.Resolve<IBus>();
        }
        [TestCleanup]
        public void Cleanup()
        {
            _container.Dispose();
        }

        [TestMethod]
        public void AddProductToCart()
        {
            var customerController = _container.Resolve<CustomerController>();
            var productController = _container.Resolve<ProductController>();
            var cartController = _container.Resolve<CartController>();

            var customersViewResult = customerController.Index() as ViewResult;
            var productsViewResult = productController.Index() as ViewResult;

            var model = new AddProductViewModel()
            {
                CustomerId = ((List<Customer>)customersViewResult.Model)[0].Id,
                ProductId = ((List<Product>)productsViewResult.Model)[0].Id,
                Quantity = 1
            };

            var result = cartController.AddProduct(model) as RedirectToRouteResult;
        }

        [TestMethod]
        public void RemoveProductFromCart()
        {
            var customerController = _container.Resolve<CustomerController>();
            var productController = _container.Resolve<ProductController>();
            var cartController = _container.Resolve<CartController>();

            var customersViewResult = customerController.Index() as ViewResult;
            var productsViewResult = productController.Index() as ViewResult;

            var model = new RemoveProductViewModel()
            {
                CustomerId = ((List<Customer>)customersViewResult.Model)[0].Id,
                ProductId = ((List<Product>)productsViewResult.Model)[0].Id
            };

            var result = cartController.RemoveProduct(model) as RedirectToRouteResult;
        }

        [TestMethod]
        public void ApplyVoucherToCart()
        {
            var customerController = _container.Resolve<CustomerController>();
            var cartController = _container.Resolve<CartController>();

            var customersViewResult = customerController.Index() as ViewResult;

            var model = new ApplyVoucherViewModel()
            {
                CustomerId = ((List<Customer>)customersViewResult.Model)[0].Id,
                VoucherCode = "ABCD"
            };

            var result = cartController.ApplyVoucher(model) as RedirectToRouteResult;
        }

        [TestMethod]
        public void CheckoutCart()
        {
            var customerController = _container.Resolve<CustomerController>();
            var controller = _container.Resolve<CartController>();

            var customersViewResult = customerController.Index() as ViewResult;

            var model = new CheckoutViewModel()
            {
                CustomerId = ((List<Customer>)customersViewResult.Model)[0].Id,
                Phone = "123456",
                City = "Varna",
                Address = "Varna"
            };
            var result = controller.Checkout(model) as RedirectToRouteResult;
        }
    }
}
