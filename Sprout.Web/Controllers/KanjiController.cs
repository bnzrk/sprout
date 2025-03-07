using Microsoft.AspNetCore.Mvc;
using Sprout.Web.Data.Entities.Kanji;
using Sprout.Web.Data.Importers;
using Sprout.Web.Services;
using System.Text;

namespace Sprout.Web.Controllers
{
    [ApiController]
    [Route("api/v1/kanji")]
    public class KanjiController : Controller
    {
        private readonly IKanjiService _kanjiService;

        public KanjiController(IKanjiService kanjiService)
        {
            _kanjiService = kanjiService;
        }

        // Returns an entry for a kanji by its character literal.
        [HttpGet("{literal}")]
        public async Task<IActionResult> GetByLiteral(string literal)
        {
            var kanji = await _kanjiService.GetKanjiByLiteralAsync(literal);
            if (kanji == null)
            {
                return NotFound();
            }
            return Ok(kanji);
        }

        // Returns an array of kanji entries by an array of kanji literals.
        [HttpPost("batch")]
        public async Task<IActionResult> GetByLiterals([FromBody] string[] literals)
        {
            Console.Write("literals recieved: ");
            foreach (var l in literals)
            {
                Console.WriteLine(l);
            }
            if (literals == null || literals.Length == 0)
            {
                return BadRequest("Literals array cannot be empty.");
            }

            var kanjiList = await _kanjiService.GetKanjiByLiteralsAsync(literals);

            if (kanjiList == null || !kanjiList.Any())
            {
                return NotFound("No matching kanji found.");
            }

            return Ok(kanjiList);
        }

        // TEMP: Requests population of the kanji dictionary database
        [HttpPost("populate")]
        public async Task<IActionResult> PopulateDictionary()
        {
            Console.Write("Attempting to populate database...");

            await _kanjiService.PopulateDbAsync();
            return Ok("Dictionary database populated successfully.");
        }
    }
}
