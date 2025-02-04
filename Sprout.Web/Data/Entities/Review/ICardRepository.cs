namespace Sprout.Web.Data.Entities.Review
{
    public interface ICardRepository
    {
        Task SaveCardAsync(Card card);
        Task SaveCardListAsync(List<Card> cardList);
        Task UpdateCardAsync(Card card);
        Task DeleteCardAsync(Card card);
        Task<IEnumerable<Card>> GetCardsWithoutReviewAsync(string userId);
        Task<IEnumerable<Card>> GetCardsDueOnAsync(string userId, DateTime dueDateTime);
        Task<int> GetCardsWithoutReviewCountAsync(string userId);
        Task<int> GetCardsDueOnCountAsync(string userId, DateTime dueDateTime);
        Task<Card> GetCardByIdAsync(int id);
        Task<Card> GetCardByKanjiAsync(string userId, string kanjiLiteral);
        Task<bool> SaveAllAsync();
    }
}
