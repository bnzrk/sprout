using Sprout.Web.Contracts;
using Sprout.Web.Data.Entities.Review;

namespace Sprout.Web.Services
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _deckRepository;

        public DeckService(IDeckRepository deckRepository)
        {
            _deckRepository = deckRepository;
        }

        public async Task AddCardToDeckAsync(Deck deck, Card card)
        {
            deck.Cards.Add(card);
            await _deckRepository.UpdateDeckAsync(deck);
            await _deckRepository.SaveAllAsync();
        }

        public async Task<int> CreateDeckAsync(string userId, string name)
        {
            var deck = new Deck
            {
                Name = name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };
            await _deckRepository.SaveDeckAsync(deck);
            return deck.Id;
        }

        public async Task DeleteDeckAsync(Deck deck)
        {
            await _deckRepository.DeleteDeckAsync(deck);
        }

        public async Task<Deck> GetDeckByIdAsync(int deckId)
        {
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck == null)
            {
                throw new Exception("Deck not found.");
            }
            return deck;
        }

        public async Task<List<Card>> GetDeckDueCardsAsync(int deckId, DateTime dueDateTime)
        {
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck == null)
            {
                throw new Exception("Deck not found.");
            }
            var newCards = (await _deckRepository.GetDeckCardsWithoutReviewAsync(deckId)).ToList();
            var dueCards = (await _deckRepository.GetDeckCardsDueOnAsync(deckId, dueDateTime)).ToList();
            dueCards.AddRange(newCards);
            return dueCards;
        }

        public async Task<DeckReviewSummaryDTO> GetDeckReviewSummaryAsync(int deckId, DateTime dueDateTime)
        {
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck == null)
            {
                throw new Exception("Deck not found.");
            }
            var newCards = (await _deckRepository.GetDeckCardsWithoutReviewAsync(deckId)).ToList();
            var dueCards = (await _deckRepository.GetDeckCardsDueOnAsync(deckId, dueDateTime)).ToList();
            var summary = new DeckReviewSummaryDTO
            {
                DeckName = deck.Name,
                CardReviewSummary = new CardReviewSummaryDTO
                {
                    New = newCards.Count(),
                    Due = newCards.Count()
                }
            };
            return summary;
        }

        public async Task RemoveCardFromDeckAsync(Deck deck, Card card)
        {
            deck.Cards.Remove(card);
            await _deckRepository.UpdateDeckAsync(deck);
            await _deckRepository.SaveAllAsync();
        }

        public async Task RenameDeckAsync(Deck deck, string name)
        {
            // TODO: validation
            deck.Name = name;
            await _deckRepository.UpdateDeckAsync(deck);
        }
    }
}
