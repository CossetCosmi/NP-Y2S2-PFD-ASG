using PFD.Model;
using PFD.Repository.Interface;
using PFD.Repository.RowMapper;

namespace PFD.Repository
{
    internal sealed class TransactionRepository : Repository<Transaction, int>, ITransactionRepository
    {
        #region Singleton
        private TransactionRepository() { }

        public static TransactionRepository Instance = Singleton.Instance;

        private class Singleton
        {
            static Singleton() { }

            internal static readonly TransactionRepository Instance = new TransactionRepository();
        }
        #endregion Singleton

        protected override string Table => "Transaction";
        protected override string Id => "Id";
        protected override IRowMapper<Transaction> RowMapper => TransactionRowMapper.Instance;
    }
}
