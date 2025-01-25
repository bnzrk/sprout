using Sprout.Web.Data.Entities.Review;

namespace Sprout.Web.Contracts
{
    public class SrsDataDto
    {
        required public int Id { get; set; }
        required public int CardId { get; set; }
        required public int ProgressLevel { get; set; } = 1;
        public DateTime? FirstReview { get; set; }
        public DateTime? LastReview { get; set; }
        public DateTime? NextReview { get; set; }
        public bool IsMastered { get; set; } = false;
    }
}
