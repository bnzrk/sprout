using Sprout.Web.Contracts;
using Sprout.Web.Data.Entities.Srs;
using Sprout.Web.Data.Entities.Review;

namespace Sprout.Web.Mappings
{
    public class Mapper : IMapper
    {
        public CardDto MapCardToDto(Card card)
        {
            return new CardDto
            {
                Id = card.Id,
                UserId = card.UserId,
                Kanji = card.Kanji,
                SrsData = MapSrsDataToDto(card.SrsData)
            };
        }

        public DeckDto MapDeckToDto(Deck deck)
        {
            return new DeckDto();
        }

        public SrsDataDto MapSrsDataToDto(SrsData srsData)
        {
            return new SrsDataDto
            {
                Id = srsData.Id,
                CardId = srsData.CardId,
                ProgressLevel = srsData.ProgressLevel,
                FirstReview = srsData.FirstReview,
                LastReview = srsData.LastReview,
                NextReview = srsData.NextReview,
                IsMastered = srsData.IsMastered
            };
        }
    }
}
