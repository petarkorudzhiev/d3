using d3.Framework.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Framework.Events
{
    public static class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate> _actions;
        public static IBus Bus;

        public static void Register<T>(Action<T> callback) where T : IEvent
        {
            if (_actions == null)
                _actions = new List<Delegate>();

            _actions.Add(callback);
        }

        public static void Raise<T>(T domainEvent) where T : IEvent
        {
            if (Bus != null)
                Bus.Publish(domainEvent);

            if (_actions != null)
                foreach (var action in _actions)
                    if (action is Action<T>)
                        ((Action<T>)action)(domainEvent);
        }
    }
}
