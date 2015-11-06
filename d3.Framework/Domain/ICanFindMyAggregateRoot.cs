using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Framework.Domain
{
    public interface ICanFindMyAggregateRoot
    {
        AggregateRoot MyRoot { get; }
    }
}
