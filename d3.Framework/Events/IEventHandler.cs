using d3.Framework.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Framework.Events
{
    public interface IEventHandler<T> : IHandler<T> where T : IEvent
    {
    }
}
