namespace Sprout.Web.Contracts
{
    public class DeckReviewSummaryDto
    {
        required public string DeckName { get; set; }
        required public CardReviewSummaryDto CardReviewSummary { get; set; }
    }
}
