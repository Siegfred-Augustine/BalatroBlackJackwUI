using Microsoft.AspNetCore.Mvc;

namespace BlackJackBalatro.Controllers
{
    public class GameAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
