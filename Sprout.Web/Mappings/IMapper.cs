using Sprout.Web.Contracts;
using Sprout.Web.Data.Entities.Review;
using Sprout.Web.Data.Entities.Srs;

namespace Sprout.Web.Mappings
{
    public interface IMapper
    {
        SrsDataDto MapSrsDataToDto(SrsData srsData);
        CardDto MapCardToDto(Card card);
        DeckDto MapDeckToDto(Deck deck);
    }
}
