using Microsoft.AspNetCore.Mvc;

namespace CalmSpire.Controllers
{
    public class MeditationController : Controller
    {
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}