using PFD.Model;
using PFD.Repository.Interface;
using PFD.Repository.RowMapper;

namespace PFD.Repository
{
    internal sealed class TransactionRepository : Repository<Transaction, int>, ITransactionRepository
    {
        protected override string Table => "Transaction";
        protected override string Id => "Id";
        protected override IRowMapper<Transaction> RowMapper => TransactionRowMapper.Instance;
    }
}
