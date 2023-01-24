using System.Data.SqlClient;

namespace PFD.Repository.Interface
{
    internal interface IRowMapper<MODEL>
    {
        MODEL Convert(SqlDataReader reader);
    }
}
