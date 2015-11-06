using Castle.Windsor;
using d3.Framework.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace d3.WebUI.Config
{
    public class CastleWindsorServiceLocator : IServiceLocator
    {
        private IWindsorContainer _container;

        public CastleWindsorServiceLocator(IWindsorContainer container)
        {
            _container = container;
        }

        public T GetService<T>()
        {
            return _container.Resolve<T>();
        }

        public object GetService(Type type)
        {
            return _container.Resolve(type);
        }
    }
}