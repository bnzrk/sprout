using Sprout.Web.Contracts;
using Sprout.Web.Data.Entities.Review;

namespace Sprout.Web.Services
{
    public interface IDeckService
    {
        Task<Deck> GetDeckByIdAsync(int deckId);
        Task CreateDeckAsync(string name);
        Task RenameDeckAsync(Deck deck, string name);
        Task DeleteDeckAsync(Deck deck);
        Task AddCardAsync(Deck deck, Card card);
        Task AddCardListAsync(Deck deck, List<Card> cardList);
        Task RemoveCardAsync(Deck deck, Card card);
        Task<DeckReviewSummaryDTO> GetReviewSummaryAsync(Deck deck,DateTime dueDateTime);
        Task<List<Card>> GetDueCardsAsync(int deckId, DateTime dueDateTime);
        Task<List<Card>> GetNewCardsAsync(int deckId, DateTime dueDateTime);
    }
}
