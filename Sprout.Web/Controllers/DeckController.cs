using Microsoft.AspNetCore.Mvc;
using Sprout.Web.Services;

namespace Sprout.Web.Controllers
{
    // TODO: Replace BadRequest with appropriate responses
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
            // TODO: Use auth token to get user from user microservice
            string userId = "";
            int deckId = await _deckService.CreateDeckAsync(userId, name);
            return CreatedAtAction(nameof(GetDeck), new { id = deckId }, null);
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
        public async Task<IActionResult> GetDeckDueCards(int id)
        {
            try
            {
                var dueCards = await _deckService.GetDeckDueCardsAsync(id, DateTime.Now);
                return Ok(dueCards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/add/{kanji}")]
        public async Task<IActionResult> AddCardToDeck(int id, string kanji)
        {
            try
            {
                var deck = await _deckService.GetDeckByIdAsync(id);
                var card = await _cardService.GetCardbyKanjiAsync(kanji);
                await _deckService.AddCardToDeckAsync(deck, card);
                return Ok("Card added to deck successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/remove/{kanji}")]
        public async Task<IActionResult> RemoveCardFromDeck(int id, string kanji)
        {
            try
            {
                var deck = await _deckService.GetDeckByIdAsync(id);
                var card = await _cardService.GetCardbyKanjiAsync(kanji);
                await _deckService.RemoveCardFromDeckAsync(deck, card);
                return Ok("Card removed from deck successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }   
        }

        [HttpPut("{id}/rename/{name}")]
        public async Task<IActionResult> RenameDeck(int id, string name)
        {
            try
            {
                var deck = await _deckService.GetDeckByIdAsync(id);
                await _deckService.RenameDeckAsync(deck, name);
                return StatusCode(204, "Deck created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}/review-summary")]
        public async Task<IActionResult> GetDeckReviewSummary(int id)
        {
            try
            {
                var summary = await _deckService.GetDeckReviewSummaryAsync(id, DateTime.Now);
                if (summary == null)
                {
                    return NotFound();
                }
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
