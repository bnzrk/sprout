﻿using Sprout.Web.Data.Entities.Review;

namespace Sprout.Web.Data.Entities.Srs
{
    public class SrsData
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int ProgressLevel { get; set; } = 1;
        public DateTime? FirstReview { get; set; }
        public DateTime? LastReview { get; set; }
        public DateTime? NextReview { get; set; }
        public bool IsMastered { get; set; } = false;
        public Card Card { get; set; }
    }
}
