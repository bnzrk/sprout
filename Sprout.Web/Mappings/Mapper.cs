using Sprout.Web.Contracts;
using Sprout.Web.Data.Entities.Srs;
using Sprout.Web.Data.Entities.Review;
using Sprout.Web.Data.Entities.Kanji;

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

        public SimpleKanjiDto MapKanjiToSimpleDto(Kanji kanji)
        {
            return new SimpleKanjiDto
            {
                Literal = kanji.Literal,
                Meanings = kanji.Meanings,
                KunReadings = kanji.KunReadings,
                OnReadings = kanji.OnReadings
            };
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
