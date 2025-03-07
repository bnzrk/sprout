using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Sprout.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ReviewController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
