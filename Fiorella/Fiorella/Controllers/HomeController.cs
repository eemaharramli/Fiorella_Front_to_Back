using Microsoft.AspNetCore.Mvc;

namespace Fiorella.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}