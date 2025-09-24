using Microsoft.AspNetCore.Mvc;

namespace CalmSpire.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
