using Sprout.Web.Contracts;

namespace Sprout.Web.Models
{
    public class HomeViewModel
    {
        public CardReviewSummaryDto CardReviewSummary { get; set; }
        public List<DeckReviewSummaryDto> DeckReviewSummaries { get; set; } = new List<DeckReviewSummaryDto>();
    }
}
