using Sprout.Web.Data.Entities.Review;
using Sprout.Web.Contracts;

namespace Sprout.Web.Services
{
    public interface ICardService
    {
        Task<CardDto> GetCardByIdAsync(int cardId);
        Task<CardDto> GetCardByKanjiAsync(string kanjiLiteral);
        Task<List<CardDto>> GetDueCardsAsync(DateTime dueDateTime);
        Task CreateCardAsync(string kanjiLiteral);
        Task DeleteCardAsync(int cardId);
        Task<CardReviewSummaryDto> GetReviewSummaryAsync(DateTime dueDateTime);
    }
}
