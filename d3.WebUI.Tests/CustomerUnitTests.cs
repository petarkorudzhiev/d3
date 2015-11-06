using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using d3.WebUI.Config;
using d3.WebUI.Controllers;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Context;
using d3.Framework.Events;
using d3.Framework.Bus;
using d3.Sales.Application.CommandModels.Customer;
using Castle.Windsor;

namespace d3.WebUI.Tests
{
    [TestClass]
    public class CustomerUnitTests
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
        public void CreateCustomer()
        {
            var controller = _container.Resolve<CustomerController>();
            var model = new CreateCustomerViewModel()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov"
            };

            var result = controller.Create(model) as RedirectToRouteResult;
        }
    }
}
