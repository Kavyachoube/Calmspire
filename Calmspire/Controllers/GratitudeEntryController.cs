// Controllers/GratitudeEntryController.cs
using CalmSpire.Data;
using CalmSpire.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalmSpire.Controllers
{
    public class GratitudeEntryController : Controller
    {
        private readonly CalmSpireDbContext _db;
        public GratitudeEntryController(CalmSpireDbContext db) { _db = db; }

        public IActionResult Index()
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (!uid.HasValue) return RedirectToAction("Login", "Account");

            var list = _db.GratitudeEntries
                .Where(e => e.UserId == uid.Value)
                .OrderByDescending(e => e.CreatedAt)
                .ToList();

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string content)
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (!uid.HasValue) return RedirectToAction("Login", "Account");

            if (!string.IsNullOrWhiteSpace(content))
            {
                var entry = new GratitudeEntry
                {
                    UserId = uid.Value,
                    Content = content,
                    CreatedAt = DateTime.UtcNow
                };
                _db.GratitudeEntries.Add(entry);
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
