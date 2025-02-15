using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Sprout.Web.Models;
using Sprout.Web.Contracts;
using System.Diagnostics;

namespace Sprout.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5152");
            _logger = logger;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IActionResult> Index()
        {
            var cardResponse = await _httpClient.GetAsync("api/v1/cards/review-summary");
            var deckResponse = await _httpClient.GetAsync("api/v1/decks/review-summary");
            if (!cardResponse.IsSuccessStatusCode || !deckResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to get summaries.");
                return View();
            }

            var cardSummary = await cardResponse.Content.ReadFromJsonAsync<CardReviewSummaryDto>(_jsonSerializerOptions);
            var deckSummary = await deckResponse.Content.ReadFromJsonAsync<List<DeckReviewSummaryDto>>(_jsonSerializerOptions);
            var model = new HomeViewModel
            {
                CardReviewSummary = cardSummary,
                DeckReviewSummaries = deckSummary
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
