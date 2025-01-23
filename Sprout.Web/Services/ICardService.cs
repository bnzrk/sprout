using Sprout.Web.Data.Entities.Review;
using Sprout.Web.Contracts;

namespace Sprout.Web.Services
{
    public interface ICardService
    {
        Task<Card> GetCardByIdAsync(int cardId);
        Task<Card> GetCardbyKanjiAsync(string kanjiLiteral);
        Task<List<Card>> GetDueCardsAsync(DateTime dueDateTime);
        Task CreateCardAsync(string kanjiLiteral);
        Task DeleteCardAsync(Card card);
        Task<CardReviewSummaryDTO> GetReviewSummaryAsync(DateTime dueDateTime);
    }
}
