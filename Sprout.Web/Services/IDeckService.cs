using Sprout.Web.Contracts;
using Sprout.Web.Data.Entities.Review;

namespace Sprout.Web.Services
{
    public interface IDeckService
    {
        Task<Deck> GetDeckByIdAsync(int deckId);
        Task<int> CreateDeckAsync(string userId, string name);
        Task RenameDeckAsync(Deck deck, string name);
        Task DeleteDeckAsync(Deck deck);
        Task AddCardToDeckAsync(Deck deck, Card card);
        Task RemoveCardFromDeckAsync(Deck deck, Card card);
        Task<DeckReviewSummaryDTO> GetDeckReviewSummaryAsync(int deckId, DateTime dueDateTime);
        Task<List<Card>> GetDeckDueCardsAsync(int deckId, DateTime dueDateTime);
    }
}
