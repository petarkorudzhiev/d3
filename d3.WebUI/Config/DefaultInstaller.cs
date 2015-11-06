using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using d3.Framework.Bus;
using d3.Sales.Application.AppServices;
using d3.Sales.Domain.Products;
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

namespace d3.WebUI.Config
{
    public class DefaultInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBus>().ImplementedBy<InThreadBus>());
            container.Register(Component.For<IProductAppService>().ImplementedBy<ProductAppService>().LifestylePerWebRequest());
            container.Register(Component.For<IProductRepository>().ImplementedBy<ProductRepository>().LifestylePerWebRequest());
            container.Register(Component.For<ISessionFactory>().UsingFactoryMethod(() => CreateNhSessionFactory()).LifestyleSingleton());
            container.Register(Component.For<ISession>().UsingFactory<ISessionFactory, ISession>(factory => factory.OpenSession()).LifestylePerWebRequest());
        }

        public static ISessionFactory CreateNhSessionFactory()
        {
            var connStr = ConfigurationManager.ConnectionStrings["SalesDatabaseContext"].ConnectionString;

            var configuration = Fluently.Configure()
                                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connStr))
                                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetAssembly(typeof(CartMap))))
                                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                                .ExposeConfiguration(cfg => cfg.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { new ForceRootAggregateUpdateListener() })
                                .ExposeConfiguration(cfg => cfg.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { new ForceRootAggregateUpdateListener() });
            
            return configuration.BuildSessionFactory();
        }
    }
}