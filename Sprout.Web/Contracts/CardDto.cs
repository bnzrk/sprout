using Sprout.Web.Data.Entities.Review;
using Sprout.Web.Data.Entities.Srs;

namespace Sprout.Web.Contracts
{
    public class CardDto
    {
        required public int Id { get; set; }
        required public string UserId { get; set; }
        required public string Kanji { get; set; }
        required public SrsDataDto SrsData { get; set; }
    }
}
