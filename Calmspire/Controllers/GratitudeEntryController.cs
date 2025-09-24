using CalmSpire.Models;
using CalmSpire.Data;   // yeh tumhare DbContext ka namespace hai
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CalmSpire.Controllers
{
    [Authorize]
    public class GratitudeEntryController : Controller
    {
        private readonly CalmSpireDbContext _db;

        public GratitudeEntryController(CalmSpireDbContext db)
        {
            _db = db;
        }

        // List all gratitude entries of current user
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var entries = _db.GratitudeEntries
                .Where(g => g.UserId == userId)
                .OrderByDescending(g => g.CreatedAt)
                .ToList();

            return View(entries);
        }

        // GET: Create new entry
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Save entry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GratitudeEntry model)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

                model.UserId = userId;
                model.CreatedAt = DateTime.Now;

                _db.GratitudeEntries.Add(model);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // DELETE entry
        public IActionResult Delete(int id)
        {
            var entry = _db.GratitudeEntries.Find(id);
            if (entry != null)
            {
                _db.GratitudeEntries.Remove(entry);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
