using Microsoft.AspNetCore.Mvc;
using Sprout.Web.Contracts;
using Sprout.Web.Services;

namespace Sprout.Web.Controllers
{
    [ApiController]
    [Route("api/v1/srs")]
    public class SrsController : Controller

    {
        private readonly ISrsService _srsService;

        public SrsController(ISrsService srsService)
        {
            _srsService = srsService;
        }

        [HttpPost("review")]
        public async Task<IActionResult> ReviewItem([FromBody] ReviewDTO reviewResult)
        {
            if (reviewResult == null || reviewResult.SrsId < 0)
            {
                return BadRequest("Invalid review.");
            }

            try
            {
                await _srsService.UpdateSrsProgressAsync(reviewResult.SrsId, reviewResult.IsCorrect);
                return Ok(new { message = "Review processed successfully." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured while attempting to process a review." });
            }
        }

        [HttpGet("{srsId}")]
        public async Task<IActionResult> GetSrsData(int srsId)
        {
            var srsData = await _srsService.GetSrsDataByIdAsync(srsId);
            if (srsData == null)
            {
                return NotFound();
            }
            return Ok(srsData);
        }
    }
}
