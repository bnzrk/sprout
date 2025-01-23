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
            var card = await _cardService.GetCardbyKanjiAsync(kanji);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] string kanji)
        {
            await _cardService.CreateCardAsync(kanji);
            return Ok("Card created successfully.");
        }

        [HttpGet("due")]
        public async Task<IActionResult> GetDueCards()
        {
            var dueCard = await _cardService.GetDueCardsAsync();
            if (dueCard == null || dueCard.Count == 0)
            {
                return NotFound();
            }
            return Ok(dueCard);
        }

        [HttpGet("review-summary")]
        public async Task<IActionResult> GetReviewSummary()
        {
            var summary = await _cardService.GetReviewSummaryAsync();
            if (summary == null)
            {
                return NotFound();
            }
            return Ok(summary);
        }
    }
}
