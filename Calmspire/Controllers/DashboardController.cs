using CalmSpire.Services;
using CalmSpire.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CalmSpire.Controllers
{
    public class DashboardController : Controller
    {
        private readonly SuggestionEngineService _suggestionEngine;
        private readonly CalmSpireDbContext _db;

        public DashboardController(SuggestionEngineService suggestionEngine, CalmSpireDbContext db)
        {
            _suggestionEngine = suggestionEngine;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            int userId = 1; // TODO: get from logged-in user session

            var suggestion = await _suggestionEngine.GetSuggestionForUserAsync(userId);
            var moods = _db.MoodEntries
                           .Where(m => m.UserId == userId)
                           .OrderByDescending(m => m.EntryDate)
                           .Take(7)
                           .ToList();

            var gratitudeCount = _db.GratitudeEntries.Count(g => g.UserId == userId);
            var journals = _db.JournalEntries
                              .Where(j => j.UserId == userId)
                              .OrderByDescending(j => j.CreatedAt)
                              .Take(3)
                              .ToList();

            ViewBag.Suggestion = suggestion;
            ViewBag.Moods = moods;
            ViewBag.GratitudeCount = gratitudeCount;
            ViewBag.Journals = journals;

            return View();
        }
    }
}
