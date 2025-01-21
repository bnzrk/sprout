using Sprout.Web.Controllers;
using Sprout.Web.Data.Entities.SRS;

namespace Sprout.Web.Data.Entities.Card
{
    public class Card
    {
        public int Id { get; set; }
        public int DeckId { get; set; }
        public string Kanji { get; set; }
        public Deck Deck { get; set; }
        public SRSData SRSData { get; set; }
    }
}
