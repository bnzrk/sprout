namespace Sprout.Web.Data.Entities.Review
{
    public class Deck
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
