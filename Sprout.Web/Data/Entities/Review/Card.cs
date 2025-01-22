using Sprout.Web.Controllers;
using Sprout.Web.Data.Entities.Srs;

namespace Sprout.Web.Data.Entities.Review
{
    public class Card
    {
        public int Id { get; set; }
        public string Kanji { get; set; }
        public ICollection<Deck> Decks { get; set; }
        public SrsData SrsData { get; set; }
    }
}
