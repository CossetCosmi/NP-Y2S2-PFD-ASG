using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFD.Repository.RowMapper
{
    public abstract class RowMapper<MODEL> : IRowMapper<MODEL>
    {
        public abstract MODEL Convert(SqlDataReader reader);

        protected T? Get<T>(object value) where T : struct
        {
            if (value is DBNull)
                return null;

            return (T)value;
        }

        protected string GetString(object value)
        {
            if (value is DBNull)
                return string.Empty;

            return (string)value;
        }
    }
}
