using PFD.Model;
using System.Data.SqlClient;

namespace PFD.Repository.RowMapper
{
    internal sealed class AccountRowMapper : RowMapper<Account>
    {
        #region Singleton
        private AccountRowMapper() { }

        public static AccountRowMapper Instance => Singleton.Instance;

        private class Singleton
        {
            static Singleton() { }

            internal static readonly AccountRowMapper Instance = new AccountRowMapper();
        }
        #endregion Singleton

        public override Account Convert(SqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string username = (string)reader["Username"];
            string password = (string)reader["Password"];
            string salt = (string)reader["Salt"];

            return new Account
            {
                Id = id,
                Username = username,
                Password = password,
                Salt = salt,
            };
        }
    }
}
