using Microsoft.AspNetCore.Mvc;

namespace Calmspire.Controllers
{
    public class AssessmentController : Controller
    {
        public IActionResult Start()
        {
            return View();
        }
        public IActionResult Questions()
        {
            return View();
        }
        public IActionResult Result()
        {
            return View();
        }
    }
}
