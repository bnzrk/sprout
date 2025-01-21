using Microsoft.AspNetCore.Mvc;

namespace Sprout.Web.Controllers
{
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
