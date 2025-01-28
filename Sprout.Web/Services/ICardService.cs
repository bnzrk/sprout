using Sprout.Web.Data.Entities.Review;
using Sprout.Web.Contracts;

namespace Sprout.Web.Services
{
    public interface ICardService
    {
        Task<CardDto> GetCardByIdAsync(int cardId);
        Task<CardDto> GetCardByKanjiAsync(string userId, string kanjiLiteral);
        Task<List<CardDto>> GetDueCardsAsync(string userId,DateTime dueDateTime);
        Task CreateCardAsync(string userId, string kanjiLiteral);
        Task DeleteCardAsync(int cardId);
        Task<CardReviewSummaryDto> GetReviewSummaryAsync(string userId, DateTime dueDateTime);
    }
}
