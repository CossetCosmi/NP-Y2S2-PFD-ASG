using PFD.Model;
using PFD.Repository.Interface;
using PFD.Repository.RowMapper;

namespace PFD.Repository
{
    internal sealed class CardRepository : Repository<Card, string>, ICardRepository
    {
        protected override string Table => "Card";
        protected override string Id => "Id";
        protected override IRowMapper<Card> RowMapper => CardRowMapper.Instance;
    }
}
