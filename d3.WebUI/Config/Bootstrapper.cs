using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using d3.Framework.Bus;
using d3.Framework.Config;
using d3.Framework.Domain;
using d3.Sales.Application.AppServices;
using d3.Sales.Domain.Carts;
using d3.Sales.Domain.Customers;
using d3.Sales.Domain.Customers.Handlers;
using d3.Sales.Domain.Products;
using d3.Sales.Domain.Purchases;
using d3.Sales.Domain.Vouchers;
using d3.Sales.Infrastructure.Mappers;
using d3.Sales.Infrastructure.Repositories;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace d3.WebUI.Config
{
    public static class Bootstrapper
    {

        public static IWindsorContainer Initialize()
        {
            var container = new WindsorContainer().Install(FromAssembly.This());

            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            RegisterHandlers(new CastleWindsorServiceLocator(container));

            return container;
        }

        public static IWindsorContainer InitializeTests()
        {
            var container = new WindsorContainer();

            container.Register(Classes.FromThisAssembly().Pick().If(t => t.Name.EndsWith("Controller")).Configure(configurer => configurer.Named(configurer.Implementation.Name)).LifestylePerThread());
            container.Register(Component.For<IBus>().ImplementedBy<InThreadBus>());
            container.Register(Component.For<IProductAppService>().ImplementedBy<ProductAppService>().LifestylePerThread());
            container.Register(Component.For<IProductRepository>().ImplementedBy<ProductRepository>().LifestylePerThread());
            container.Register(Component.For<ICustomerRepository>().ImplementedBy<CustomerRepository>().LifestylePerThread());
            container.Register(Component.For<ICartRepository>().ImplementedBy<CartRepository>().LifestylePerThread());
            container.Register(Component.For<IPurchaseRepository>().ImplementedBy<PurchaseRepository>().LifestylePerThread());
            container.Register(Component.For<IVoucherRepository>().ImplementedBy<VoucherRepository>().LifestylePerThread());

            container.Register(Component.For<ISessionFactory>().UsingFactoryMethod(() => DefaultInstaller.CreateNhSessionFactory()).LifestyleSingleton());
            container.Register(Component.For<ISession>().UsingFactory<ISessionFactory, ISession>(factory => factory.OpenSession()).LifestylePerThread());

            container.Register(Classes.FromAssemblyNamed("d3.Sales.Domain").Pick().If(t => t.Name.EndsWith("EventHandler")).Configure(configurer => configurer.Named(configurer.Implementation.Name)).LifestylePerThread());

            RegisterHandlers(new CastleWindsorServiceLocator(container));

            return container;
        }

        private static void RegisterHandlers(IServiceLocator serviceLocator)
        {
            var registrar = new BusRegistrar(serviceLocator);
            registrar.Register(typeof(CartCheckedOutEventHandler));
        }
    }
}