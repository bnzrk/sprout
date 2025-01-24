namespace Sprout.Web.Data.Entities.Review
{
    public interface IDeckRepository
    {
        Task SaveDeckAsync(Deck deck);
        Task UpdateDeckAsync(Deck deck);
        Task DeleteDeckAsync(Deck deck);
        Task<IEnumerable<Card>> GetDeckCardsWithoutReviewAsync(int deckId);
        Task<IEnumerable<Card>> GetDeckCardsDueOnAsync(int deckId, DateTime dueDateTime);
        Task<IEnumerable<Card>> GetAllDeckCards(int deckId);
        Task<Deck> GetDeckByIdAsync(int deckId);
        Task<bool> SaveAllAsync();
    }
}
