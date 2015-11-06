using d3.Framework.Domain;
using NHibernate;
using NHibernate.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace d3.WebUI.Config
{
    public class ForceRootAggregateUpdateListener : IPreUpdateEventListener, IPreInsertEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent updateEvent)
        {
            var rootFinder = updateEvent.Entity as ICanFindMyAggregateRoot;
            if (rootFinder == null)
                return false;

            updateEvent.Session.Lock(rootFinder.MyRoot, LockMode.Force);

            return false;

        }

        public bool OnPreInsert(PreInsertEvent insertEvent)
        {
            var rootFinder = insertEvent.Entity as ICanFindMyAggregateRoot;
            if (rootFinder == null)
                return false;

            insertEvent.Session.Lock(rootFinder.MyRoot, LockMode.Force);

            return false;
        }
    }
}