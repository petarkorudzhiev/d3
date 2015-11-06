using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using d3.WebUI.Config;
using NHibernate.Context;
using NHibernate;
using d3.WebUI.Controllers;
using System.Web.Mvc;
using d3.Framework.Events;
using d3.Framework.Bus;
using d3.Sales.Application.CommandModels.Product;
using Castle.Windsor;

namespace d3.WebUI.Tests
{
    [TestClass]
    public class ProductUnitTests
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
        public void CreateProduct()
        {
            var controller = _container.Resolve<ProductController>();
            var model = new CreateProductViewModel()
            {
                Name = "Product 1",
                Price = 12.3m
            };

            var result = controller.Create(model) as RedirectToRouteResult;
        }
    }
}
