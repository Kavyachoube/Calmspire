using Microsoft.AspNetCore.Mvc;

namespace CalmSpire.Controllers
{
    public class MeditationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
