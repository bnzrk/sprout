using Sprout.Web.Mappings;
using Sprout.Web.Contracts;
using Sprout.Web.Data.Entities.Review;

namespace Sprout.Web.Services
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _deckRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public DeckService(IDeckRepository deckRepository, ICardRepository cardRepository, IMapper mapper)
        {
            _deckRepository = deckRepository;
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task AddCardToDeckAsync(int deckId, int cardId)
        {
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            var card = await _cardRepository.GetCardByIdAsync(cardId);
            if (deck == null || card == null)
            {
                return;
            }
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

        public async Task DeleteDeckAsync(int deckId)
        {
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck == null)
            {
                return;
            }
            await _deckRepository.DeleteDeckAsync(deck);
        }

        public async Task<DeckDto> GetDeckByIdAsync(int deckId)
        {
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck == null)
            {
                throw new Exception("Deck not found.");
            }
            return _mapper.MapDeckToDto(deck);
        }

        public async Task<List<CardDto>> GetDeckDueCardsAsync(int deckId, DateTime dueDateTime)
        {
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck == null)
            {
                throw new Exception("Deck not found.");
            }
            var newCards = (await _deckRepository.GetDeckCardsWithoutReviewAsync(deckId)).ToList();
            var dueCards = (await _deckRepository.GetDeckCardsDueOnAsync(deckId, dueDateTime)).ToList();
            dueCards.AddRange(newCards);
            List<CardDto> cardDtos = dueCards.Select(card => _mapper.MapCardToDto(card)).ToList();
            return cardDtos;
        }

        public async Task<DeckReviewSummaryDto> GetDeckReviewSummaryAsync(int deckId, DateTime dueDateTime)
        {
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck == null)
            {
                throw new Exception("Deck not found.");
            }
            var newCards = (await _deckRepository.GetDeckCardsWithoutReviewAsync(deckId)).ToList();
            var dueCards = (await _deckRepository.GetDeckCardsDueOnAsync(deckId, dueDateTime)).ToList();
            var summary = new DeckReviewSummaryDto
            {
                DeckName = deck.Name,
                CardReviewSummary = new CardReviewSummaryDto
                {
                    New = newCards.Count(),
                    Due = newCards.Count()
                }
            };
            return summary;
        }

        public async Task RemoveCardFromDeckAsync(int deckId, int cardId)
        {
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            var card = await _cardRepository.GetCardByIdAsync(cardId);
            if (deck == null || card == null)
            {
                return;
            }
            deck.Cards.Remove(card);
            await _deckRepository.UpdateDeckAsync(deck);
            await _deckRepository.SaveAllAsync();
        }

        public async Task RenameDeckAsync(int deckId, string name)
        {
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck == null)
            {
                return;
            }
            // TODO: validation
            deck.Name = name;
            await _deckRepository.UpdateDeckAsync(deck);
        }
    }
}
