namespace Sprout.Web.Data.Entities.Srs
{
    public class SrsData
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int ProgressLevel { get; set; }
        public DateTime FirstReview { get; set; }
        public DateTime LastReviewed { get; set; }
        public DateTime NextReview { get; set; }
        public bool IsMastered { get; set; } = false;
    }
}
