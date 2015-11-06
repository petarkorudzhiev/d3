using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Framework.Domain
{
    public abstract class AggregateRoot : Entity
    {
        public virtual int Version { get; protected set; }
    }
}
