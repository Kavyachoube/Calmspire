using Microsoft.AspNetCore.Mvc;

namespace Calmspire.Controllers
{
    public class JournalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
    }
}
