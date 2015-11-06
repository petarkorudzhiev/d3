using d3.Sales.Domain.Products;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.Type;
using NHibernate.UserTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Infrastructure.Mappers.UserTypes
{
    public class MoneyUserType : ImmutableUserType<Money>
    {
        public override SqlType[] SqlTypes
        {
            get { return new[] { new SqlType(DbType.Decimal) }; }
        }
        public override object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            object obj = NHibernateUtil.Decimal.NullSafeGet(rs, names[0]);
            if (obj == null)
            {
                return null;
            }
            return new Money((decimal)obj);
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
                var decimalValue = ((Money)value).Value;
                ((IDataParameter)cmd.Parameters[index]).Value = decimalValue;
            }
        }
    }
}
