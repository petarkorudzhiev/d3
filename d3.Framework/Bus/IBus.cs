using d3.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Framework.Bus
{
    public interface IBus : IEventPublisher, IHandlerRegistrar
    {
    }
}
