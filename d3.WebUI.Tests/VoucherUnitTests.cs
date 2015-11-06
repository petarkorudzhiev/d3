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
using d3.Sales.Application.CommandModels.Voucher;
using Castle.Windsor;

namespace d3.WebUI.Tests
{
    [TestClass]
    public class VoucherUnitTests
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
        public void CreateVoucher()
        {
            var voucherController = _container.Resolve<VoucherController>();
            var customerController = _container.Resolve<CustomerController>();

            var customersIndexView = customerController.Index() as ViewResult;
            var customers = ((List<Customer>)customersIndexView.Model);

            var model = new CreateVoucherViewModel()
            {
                CustomerId = customers[0].Id,
                Amount = 10m
            };

            var result = voucherController.Create(model) as RedirectToRouteResult;
        }
    }
}
