using d3.Sales.Domain.Vouchers;
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
    public class VoucherCodeUserType : ImmutableUserType<VoucherCode>
    {
        public override NHibernate.SqlTypes.SqlType[] SqlTypes
        {
            get { return new[] { new SqlType(DbType.String) }; }
        }

        public override object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
        {
            object obj = NHibernateUtil.String.NullSafeGet(rs, names[0]);
            if (obj == null)
            {
                return null;
            }
            return new VoucherCode((string)obj);
        }

        public override void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        {
            Debug.Assert(cmd != null);
            if (value == null)
            {
                ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
            }
            else
            {
                var stringValue = ((VoucherCode)value).ToString();
                ((IDataParameter)cmd.Parameters[index]).Value = stringValue;
            }
        }
    }
}
