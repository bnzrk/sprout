using Microsoft.AspNetCore.Mvc;
using Sprout.Web.Services;

namespace Sprout.Web.Controllers
{
    [ApiController]
    [Route("api/v1/cards")]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        //[Authorize]
        [HttpGet("{kanji}")]
        public async Task<IActionResult> GetCard(string kanji)
        {
            var userId = "test-user-id";
            //var userId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var card = await _cardService.GetCardByKanjiAsync(userId, kanji);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        //[Authorize]
        [HttpPost("{kanji}")]
        public async Task<IActionResult> CreateCard(string kanji)
        {
            var userId = "test-user-id";
            await _cardService.CreateCardAsync(userId, kanji);
            return Ok("Card created successfully.");
        }

        //[Authorize]
        [HttpGet("due")]
        public async Task<IActionResult> GetDueCards()
        {
            var userId = "test-user-id";
            var dueCards = await _cardService.GetDueCardsAsync(userId, DateTime.Now);
            if (dueCards == null || dueCards.Count == 0)
            {
                return NotFound();
            }
            return Ok(dueCards);
        }

        //[Authorize]
        [HttpGet("review-summary")]
        public async Task<IActionResult> GetReviewSummary()
        {
            var userId = "test-user-id";
            var summary = await _cardService.GetReviewSummaryAsync(userId, DateTime.Now);
            if (summary == null)
            {
                return NotFound();
            }
            return Ok(summary);
        }
    }
}
