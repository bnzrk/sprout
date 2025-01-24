using Sprout.Web.Controllers;
using Sprout.Web.Data.Entities.Srs;

namespace Sprout.Web.Data.Entities.Review
{
    public class Card
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Kanji { get; set; }
        public ICollection<Deck> Decks { get; set; } = new List<Deck>();
        public SrsData SrsData { get; set; }
    }
}
