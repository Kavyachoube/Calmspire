using CalmSpire.Models.ViewModels;
using CalmSpire.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalmSpire.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthenticationService _auth;
        public AccountController(AuthenticationService auth) => _auth = auth;

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetInt32("UserId").HasValue) return RedirectToAction("Index", "Dashboard");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.Password != model.ConfirmPassword) { ModelState.AddModelError("ConfirmPassword", "Passwords must match"); return View(model); }

            try
            {
                var user = await _auth.RegisterAsync(model.Username, model.Email, model.Password);
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.Username);
                return RedirectToAction("Index", "Dashboard");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserId").HasValue) return RedirectToAction("Index", "Dashboard");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _auth.AuthenticateAsync(model.Email, model.Password);
            if (user == null) { ModelState.AddModelError("", "Invalid email or password."); return View(model); }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Username);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
