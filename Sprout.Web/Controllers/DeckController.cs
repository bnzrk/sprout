using Microsoft.AspNetCore.Mvc;
using Sprout.Web.Contracts;
using Sprout.Web.Services;

namespace Sprout.Web.Controllers
{
    [ApiController]
    [Route("api/v1/decks")]
    public class DeckController : Controller
    {
        private readonly IDeckService _deckService;
        private readonly ICardService _cardService;

        public DeckController(IDeckService deckService, ICardService cardService)
        {
            _deckService = deckService;
            _cardService = cardService;
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> CreateDeck(string name)
        {
            await _deckService.CreateDeckAsync(name);
            return Ok("Deck created successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeck(int id)
        {
            var deck = await _deckService.GetDeckByIdAsync(id);
            if (deck == null)
            {
                return NotFound();
            }
            return Ok(deck);
        }

        [HttpGet("{id}/due")]
        public async Task<IActionResult> GetDueCards(int id)
        {
            var deck = await _deckService.GetDeckByIdAsync(id);
            if (deck == null)
            {
                return NotFound();
            }
            var dueCards = await _deckService.GetDueCardsAsync(id, DateTime.Now);
            if (dueCards == null)
            {
                return NotFound();
            }
            return Ok(dueCards);
        }

        [HttpPost("{id}/add/{kanji}")]
        public async Task<IActionResult> AddCard(int id, string kanji)
        {
            var deck = await _deckService.GetDeckByIdAsync(id);
            if (deck == null)
            {
                return NotFound();
            }
            var card = await _cardService.GetCardbyKanjiAsync(kanji);
            if (card == null)
            {
                return NotFound();
            }
            await _deckService.AddCardAsync(deck, card);
            return Ok("Card added to deck successfully.");
        }

        [HttpDelete("{id}/remove/{kanji}")]
        public async Task<IActionResult> RemoveCard(int id, string kanji)
        {
            var deck = await _deckService.GetDeckByIdAsync(id);
            if (deck == null)
            {
                return NotFound();
            }
            var card = await _cardService.GetCardbyKanjiAsync(kanji);
            if (card == null)
            {
                return NotFound();
            }
            await _deckService.RemoveCardAsync(deck, card);
            return Ok("Card removed from deck successfully.");
        }

        [HttpPut("{id}/rename/{name}")]
        public async Task<IActionResult> Rename(int id, string name)
        {
            var deck = await _deckService.GetDeckByIdAsync(id);
            if (deck == null)
            {
                return NotFound();
            }
            await _deckService.RenameDeckAsync(deck, name);
            return Ok("Deck named successfully.");
        }

        [HttpGet("{id}/review-summary")]
        public async Task<IActionResult> GetReviewSummary(int id)
        {
            var deck = await _deckService.GetDeckByIdAsync(id);
            if (deck == null)
            {
                return NotFound("Deck could not be found.");
            }

            var dueCards = (await _deckService.GetDueCardsAsync(id, DateTime.Now)).ToList();
            var newCards = (await _deckService.GetNewCardsAsync(id, DateTime.Now)).ToList();

            var summary = new DeckReviewSummaryDTO
            {
                DeckName = deck.Name,
                CardReviewSummary = new CardReviewSummaryDTO
                {
                    Due = dueCards.Count,
                    New = newCards.Count
                }
            };
            return Ok(summary);
        }
    }
}
