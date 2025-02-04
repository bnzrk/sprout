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
            var response = await _httpClient.GetAsync("api/v1/cards/review-summary");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to get summary.");
                return View();
            }

            var cardSummary = await response.Content.ReadFromJsonAsync<CardReviewSummaryDto>(_jsonSerializerOptions);
            var model = new HomeViewModel
            {
                CardReviewSummary = cardSummary
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
