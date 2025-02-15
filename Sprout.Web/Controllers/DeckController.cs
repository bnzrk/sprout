using Microsoft.AspNetCore.Mvc;
using Sprout.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Sprout.Web.Contracts;

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

        //[Authorize]
        [HttpPost("{name}")]
        public async Task<IActionResult> CreateDeck(string name)
        {
            var userId = "test-user-id";
            //var userId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            int deckId = await _deckService.CreateDeckAsync(userId, name);
            return CreatedAtAction(nameof(GetDeck), new { id = deckId }, null);
        }

        //[Authorize]
        [HttpGet("{deckId}")]
        public async Task<IActionResult> GetDeck(int deckId)
        {
            //var userId = User.FindFirst("userId")?.Value;
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return Unauthorized();
            //}
            var deck = await _deckService.GetDeckByIdAsync(deckId);
            if (deck == null)
            {
                return NotFound();
            }
            return Ok(deck);
        }

        //[Authorize]
        [HttpGet("{deckId}/due")]
        public async Task<IActionResult> GetDeckDueCards(int deckId)
        {
            //var userId = User.FindFirst("userId")?.Value;
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return Unauthorized();
            //}
            try
            {
                var dueCards = await _deckService.GetDeckDueCardsAsync(deckId, DateTime.Now);
                return Ok(dueCards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("{deckId}/add/{kanji}")]
        public async Task<IActionResult> AddCardToDeck(int deckId, string kanji)
        {
            var userId = "test-user-id";
            //var userId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                var card = await _cardService.GetCardByKanjiAsync(userId, kanji);
                await _deckService.AddCardToDeckAsync(deckId, card.Id);
                return Ok("Card added to deck successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpDelete("{deckId}/remove/{kanji}")]
        public async Task<IActionResult> RemoveCardFromDeck(int deckId, string kanji)
        {
            var userId = "test-user-id";
            //var userId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                var card = await _cardService.GetCardByKanjiAsync(userId, kanji);
                await _deckService.RemoveCardFromDeckAsync(deckId, card.Id);
                return Ok("Card removed from deck successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }   
        }

        //[Authorize]
        [HttpPut("{deckId}/rename/{name}")]
        public async Task<IActionResult> RenameDeck(int deckId, string name)
        {
            //var userId = User.FindFirst("userId")?.Value;
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return Unauthorized();
            //}

            try
            {
                await _deckService.RenameDeckAsync(deckId, name);
                return StatusCode(204, "Deck created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        //[Authorize]
        [HttpGet("{deckId}/review-summary")]
        public async Task<IActionResult> GetDeckReviewSummary(int deckId)
        {
            //var userId = User.FindFirst("userId")?.Value;
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return Unauthorized();
            //}

            try
            {
                var summary = await _deckService.GetDeckReviewSummaryAsync(deckId, DateTime.Now);
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

        [HttpGet("review-summary")]
        public async Task<IActionResult> GetDeckReviewSummaries()
        {
            try
            {
                //var userId = User.FindFirst("userId")?.Value;
                //if (string.IsNullOrEmpty(userId))
                //{
                //    return Unauthorized();
                //}

                var userId = "test-user-id";
                //var userId = User.FindFirst("userId")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                try
                {
                    var summaries = await _deckService.GetDeckReviewSummariesAsync(userId, DateTime.Now);
                    if (summaries == null)
                    {
                        return NotFound();
                    }
                    return Ok(summaries);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
