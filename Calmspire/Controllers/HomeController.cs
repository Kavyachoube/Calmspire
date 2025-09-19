using CalmSpire.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CalmSpire.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CalmSpire.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Redirect to dashboard if user is already logged in
            if (HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Features()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}