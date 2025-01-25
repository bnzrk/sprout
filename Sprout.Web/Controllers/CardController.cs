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

        [HttpGet("{kanji}")]
        public async Task<IActionResult> GetCard(string kanji)
        {
            var card = await _cardService.GetCardByKanjiAsync(kanji);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        [HttpPost("{kanji}")]
        public async Task<IActionResult> CreateCard(string kanji)
        {
            await _cardService.CreateCardAsync(kanji);
            return Ok("Card created successfully.");
        }

        [HttpGet("due")]
        public async Task<IActionResult> GetDueCards()
        {
            var dueCards = await _cardService.GetDueCardsAsync(DateTime.Now);
            if (dueCards == null || dueCards.Count == 0)
            {
                return NotFound();
            }
            return Ok(dueCards);
        }

        [HttpGet("review-summary")]
        public async Task<IActionResult> GetReviewSummary()
        {
            var summary = await _cardService.GetReviewSummaryAsync(DateTime.Now);
            if (summary == null)
            {
                return NotFound();
            }
            return Ok(summary);
        }
    }
}
