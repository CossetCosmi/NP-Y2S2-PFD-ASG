using PFD.Model;
using PFD.Repository.Interface;
using PFD.Repository.RowMapper;

namespace PFD.Repository
{
    internal sealed class AccountRepository : Repository<Account, int>, IAccountRepository
    {
        #region Singleton
        private AccountRepository() { }

        public static AccountRepository Instance = Singleton.Instance;

        private class Singleton
        {
            static Singleton() { }

            internal static readonly AccountRepository Instance = new AccountRepository();
        }
        #endregion Singleton

        protected override string Table => "Account";
        protected override string Id => "Id";
        protected override IRowMapper<Account> RowMapper => AccountRowMapper.Instance;
    }
}
