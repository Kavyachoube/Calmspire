using Microsoft.AspNetCore.Mvc;

namespace Calmspire.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Result()
        {
            return View();
        }
    }
}
