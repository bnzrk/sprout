using Sprout.Web.Contracts;
using Sprout.Web.Data.Entities.Review;
using Sprout.Web.Data.Entities.Srs;

namespace Sprout.Web.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task CreateCardAsync(string kanjiLiteral)
        {
            var existingCard = _cardRepository.GetCardByKanjiAsync(kanjiLiteral);
            if (existingCard != null)
            {
                throw new Exception("Card already exists.");
            }

            var card = new Card
            {
                Kanji = kanjiLiteral,
                SrsData = new SrsData()
            };

            await _cardRepository.SaveCardAsync(card);
        }

        public async Task DeleteCardAsync(Card card)
        {
            await _cardRepository.DeleteCardAsync(card);
        }

        public async Task<Card> GetCardByIdAsync(int cardId)
        {
            var card = await _cardRepository.GetCardByIdAsync(cardId);
            if (card == null)
            {
                throw new Exception("Card not found.");
            }
            return card;
        }

        public async Task<Card> GetCardbyKanjiAsync(string kanjiLiteral)
        {
            var card = await _cardRepository.GetCardByKanjiAsync(kanjiLiteral);
            if (card == null)
            {
                throw new Exception("Card not found.");
            }
            return card;
        }

        public async Task<List<Card>> GetDueCardsAsync(DateTime dueDateTime)
        {
            var newCards = (await _cardRepository.GetCardsWithoutReviewAsync()).ToList();
            var dueCards = (await _cardRepository.GetCardsDueOnAsync(dueDateTime)).ToList();

            dueCards.AddRange(newCards);
            return dueCards;
        }

        public async Task<CardReviewSummaryDTO> GetReviewSummaryAsync(DateTime dueDateTime)
        {
            var newCards = (await _cardRepository.GetCardsWithoutReviewAsync()).ToList();
            var dueCards = (await _cardRepository.GetCardsDueOnAsync(dueDateTime)).ToList();

            var summary = new CardReviewSummaryDTO
            {
                New = newCards.Count,
                Due = dueCards.Count
            };

            return summary;
        }
    }
}
