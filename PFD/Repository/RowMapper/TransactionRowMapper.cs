using PFD.Model;
using System;
using System.Data.SqlClient;

namespace PFD.Repository.RowMapper
{
    internal sealed class TransactionRowMapper : RowMapper<Transaction>
    {
        #region Singleton
        private TransactionRowMapper() { }

        public static TransactionRowMapper Instance => Singleton.Instance;

        private class Singleton
        {
            static Singleton() { }

            internal static readonly TransactionRowMapper Instance = new TransactionRowMapper();
        }
        #endregion Singleton

        public override Transaction Convert(SqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string card = (string)reader["Card"];
            decimal amount = (decimal)reader["Amount"];
            DateTime createOn = (DateTime)reader["CreateOn"];
            string type = (string)reader["Type"];
            string status = (string)reader["Status"];
            string to = (string)reader["To"];
            string from = (string)reader["From"];
            int atm = (int)reader["ATM"];

            return new Transaction
            {
                Id = id,
                Card = card,
                Amount = amount,
                CreateOn = createOn,
                Type = type,
                Status = status,
                To = to,
                From = from,
                ATM = atm,
            };
        }
    }
}
