using d3.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Mappers
{
    public class AggregateRootMap<T> : EntityMap<T> where T : AggregateRoot
    {
        public AggregateRootMap()
        {
            Version(x => x.Version);
        }
    }
}
