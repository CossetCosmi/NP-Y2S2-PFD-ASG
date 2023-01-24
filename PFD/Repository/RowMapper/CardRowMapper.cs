using PFD.Model;
using System;
using System.Data.SqlClient;

namespace PFD.Repository.RowMapper
{
    internal sealed class CardRowMapper : RowMapper<Card>
    {
        #region Singleton
        private CardRowMapper() { }

        public static CardRowMapper Instance => Singleton.Instance;

        private class Singleton
        {
            static Singleton() { }

            internal static readonly CardRowMapper Instance = new CardRowMapper();
        }
        #endregion Singleton

        public override Card Convert(SqlDataReader reader)
        {
            string id = (string)reader["Id"];
            int accountId = (int)reader["AccountId"];
            string cvv = (string)reader["CVV"];
            DateTime issueOn = (DateTime)reader["IssueOn"];
            DateTime expireOn = (DateTime)reader["ExpireOn"];
            decimal balance = (decimal)reader["Balance"];
            string status = (string)reader["Status"];
            string pin = (string)reader["Pin"];

            return new Card
            {
                Id = id,
                AccountId = accountId,
                CVV = cvv,
                IssueOn = issueOn,
                ExpireOn = expireOn,
                Balance = balance,
                Status = status,
                Pin = pin,
            };
        }
    }
}
