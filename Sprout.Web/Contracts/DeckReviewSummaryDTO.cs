namespace Sprout.Web.Contracts
{
    public class DeckReviewSummaryDTO
    {
        required public string DeckName { get; set; }
        required public CardReviewSummaryDTO CardReviewSummary { get; set; }
    }
}
