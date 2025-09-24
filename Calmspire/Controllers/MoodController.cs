using CalmSpire.Data;
using CalmSpire.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalmSpire.Controllers
{
    public class MoodController : Controller
    {
        private readonly CalmSpireDbContext _db;
        public MoodController(CalmSpireDbContext db) { _db = db; }

        public IActionResult Index()
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (!uid.HasValue) return RedirectToAction("Login", "Account");

            var entries = _db.MoodEntries.Where(m => m.UserId == uid).OrderByDescending(m => m.CreatedAt).Take(30).ToList();
            return View(entries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult Create(MoodEntry model)
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (!uid.HasValue) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid) return View(model);
            model.UserId = uid.Value;
            model.CreatedAt = DateTime.UtcNow;
            _db.MoodEntries.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (!uid.HasValue) return RedirectToAction("Login", "Account");

            var entry = _db.MoodEntries.FirstOrDefault(m => m.Id == id && m.UserId == uid.Value);
            if (entry == null) return NotFound();
            return View(entry);
        }

        [HttpPost]
        public IActionResult Edit(MoodEntry model)
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (!uid.HasValue) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid) return View(model);

            var existing = _db.MoodEntries.FirstOrDefault(m => m.Id == model.Id && m.UserId == uid.Value);
            if (existing == null) return NotFound();
            existing.MoodScore = model.MoodScore;
            existing.Notes = model.Notes;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
