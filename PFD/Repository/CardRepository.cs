using PFD.Model;
using PFD.Repository.Interface;
using PFD.Repository.RowMapper;

namespace PFD.Repository
{
    internal sealed class CardRepository : Repository<Card, string>, ICardRepository
    {
        #region Singleton
        private CardRepository() { }

        public static CardRepository Instance = Singleton.Instance;

        private class Singleton
        {
            static Singleton() { }

            internal static readonly CardRepository Instance = new CardRepository();
        }
        #endregion Singleton

        protected override string Table => "Card";
        protected override string Id => "Id";
        protected override IRowMapper<Card> RowMapper => CardRowMapper.Instance;
    }
}
