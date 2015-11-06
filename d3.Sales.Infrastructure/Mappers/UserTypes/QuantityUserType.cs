using d3.Sales.Domain.Products;
using NHibernate;
using NHibernate.SqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Mappers.UserTypes
{
    public class QuantityUserType : ImmutableUserType<Quantity>
    {
        public override SqlType[] SqlTypes
        {
            get { return new[] { new SqlType(DbType.Int32) }; }
        }
        public override object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            object obj = NHibernateUtil.Int32.NullSafeGet(rs, names[0]);
            if (obj == null)
            {
                return null;
            }
            return new Quantity((int)obj);
        }
        public override void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            Debug.Assert(cmd != null);
            if (value == null)
            {
                ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
            }
            else
            {
                var intValue = ((Quantity)value).Value;
                ((IDataParameter)cmd.Parameters[index]).Value = intValue;
            }
        }
    }
}
