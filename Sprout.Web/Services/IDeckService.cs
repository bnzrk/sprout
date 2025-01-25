using Sprout.Web.Contracts;
using Sprout.Web.Data.Entities.Review;

namespace Sprout.Web.Services
{
    public interface IDeckService
    {
        Task<DeckDto> GetDeckByIdAsync(int deckId);
        Task<int> CreateDeckAsync(string userId, string name);
        Task RenameDeckAsync(int deckId, string name);
        Task DeleteDeckAsync(int deckId);
        Task AddCardToDeckAsync(int deckId, int cardId);
        Task RemoveCardFromDeckAsync(int deckId, int cardId);
        Task<DeckReviewSummaryDto> GetDeckReviewSummaryAsync(int deckId, DateTime dueDateTime);
        Task<List<CardDto>> GetDeckDueCardsAsync(int deckId, DateTime dueDateTime);
    }
}
