using PFD.Model;
using PFD.Repository.Interface;
using PFD.Repository.RowMapper;

namespace PFD.Repository
{
    internal sealed class AccountRepository : Repository<Account, int>, IAccountRepository
    {
        protected override string Table => "Account";
        protected override string Id => "Id";
        protected override IRowMapper<Account> RowMapper => AccountRowMapper.Instance;
    }
}
