using Microsoft.AspNetCore.Mvc;

namespace Calmspire.Controllers
{
    public class ChatbotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
