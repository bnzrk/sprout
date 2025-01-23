namespace Sprout.Web.Data.Entities.Review
{
    public interface ICardRepository
    {
        Task SaveCardAsync(Card card);
        Task SaveCardList(List<Card> card);
        Task UpdateCardAsync(Card card);
        Task DeleteCardAsync(Card card);
        Task<IEnumerable<Card>> GetCardsWithoutReviewAsync();
        Task<IEnumerable<Card>> GetCardsDueOnAsync(DateTime dueDateTime);
        Task<Card> GetCardByIdAsync(int id);
        Task<Card> GetCardByKanjiAsync(string kanjiLiteral);
        Task<bool> SaveAllAsync();
    }
}
