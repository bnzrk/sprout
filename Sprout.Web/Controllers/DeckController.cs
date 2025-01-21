using Microsoft.AspNetCore.Mvc;

namespace Sprout.Web.Controllers
{
    public class DeckController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
