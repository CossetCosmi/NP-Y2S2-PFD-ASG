using System.Data.SqlClient;

namespace PFD.Repository.RowMapper
{
    internal interface IRowMapper<MODEL>
    {
        MODEL Convert(SqlDataReader reader);
    }
}
