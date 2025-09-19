using CalmSpire.Data;
using CalmSpire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalmSpire.Controllers
{
    public class MoodController : Controller
    {
        private readonly CalmSpireDbContext _context;

        public MoodController(CalmSpireDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var moodEntries = await _context.MoodEntries
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.EntryDate)
                .ToListAsync();

            return View(moodEntries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new MoodEntry
            {
                EntryDate = DateTime.Today
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MoodEntry model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if mood entry already exists for this date
            var existingEntry = await _context.MoodEntries
                .FirstOrDefaultAsync(m => m.UserId == userId && m.EntryDate.Date == model.EntryDate.Date);

            if (existingEntry != null)
            {
                ModelState.AddModelError("EntryDate", "You have already logged your mood for this date.");
                return View(model);
            }

            model.UserId = userId.Value;
            model.CreatedAt = DateTime.UtcNow;

            _context.MoodEntries.Add(model);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Mood entry saved successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var moodEntry = await _context.MoodEntries
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (moodEntry == null)
            {
                return NotFound();
            }

            return View(moodEntry);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MoodEntry model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id != model.Id)
            {
                return NotFound();
            }

            var existingEntry = await _context.MoodEntries
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (existingEntry == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            existingEntry.MoodScore = model.MoodScore;
            existingEntry.Notes = model.Notes;
            existingEntry.EntryDate = model.EntryDate;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Mood entry updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var moodEntry = await _context.MoodEntries
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (moodEntry != null)
            {
                _context.MoodEntries.Remove(moodEntry);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Mood entry deleted successfully!";
            }

            return RedirectToAction("Index");
        }
    }
}