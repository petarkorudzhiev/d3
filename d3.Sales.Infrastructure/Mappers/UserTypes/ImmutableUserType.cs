using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Mappers.UserTypes
{
    public abstract class ImmutableUserType<T> : IUserType
    {
        public abstract SqlType[] SqlTypes { get; }
        public abstract object NullSafeGet(IDataReader rs, string[] names, object owner);
        public abstract void NullSafeSet(IDbCommand cmd, object value, int index);

        public Type ReturnedType
        {
            get { return typeof(T); }
        }
        public bool IsMutable
        {
            get { return false; }
        }
        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Equals(y);
        }
        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }
        public object DeepCopy(object value)
        {
            return value;
        }
        public object Replace(object original, object target, object owner)
        {
            return original;
        }
        public object Assemble(object cached, object owner)
        {
            return cached;
        }
        public object Disassemble(object value)
        {
            return value;
        }
    }
}
