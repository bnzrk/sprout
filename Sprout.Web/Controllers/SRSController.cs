using Microsoft.AspNetCore.Mvc;
using Sprout.Web.Contracts;

namespace Sprout.Web.Controllers
{
    [ApiController]
    [Route("api/v1/srs")]
    public class SRSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("review")]
        public async Task<IActionResult> ReviewCard([FromBody] ReviewDTO reviewResult)
        {
            if (reviewResult == null || reviewResult.SRSId < 0)
            {
                return BadRequest("Invalid review.");
            }

            try
            {
                //_srsService.UpdateSRSProgress(reviewResult.SRSId, reviewResult.IsCorrect);
                return Ok(new { message = "Review processed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while attempting to process a review." });
            }
        }
    }
}
