using Microsoft.AspNetCore.Mvc;
using CalmSpire.Data;
using CalmSpire.Models.ViewModels;

namespace CalmSpire.Controllers
{
    public class DashboardController : Controller
    {
        private readonly CalmSpireDbContext _db;
        public DashboardController(CalmSpireDbContext db) { _db = db; }

        public IActionResult Index()
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (!uid.HasValue) return RedirectToAction("Login", "Account");

            var vm = new DashboardViewModel
            {
                UserName = HttpContext.Session.GetString("UserName") ?? "User",
                RecentMoods = _db.MoodEntries.Where(m => m.UserId == uid).OrderByDescending(m => m.CreatedAt).Take(5).ToList(),
                TotalJournalEntries = _db.JournalEntries.Count(j => j.UserId == uid),
                AssessmentsCompleted = _db.AssessmentResults.Count(r => r.UserId == uid),
                LastMoodEntry = _db.MoodEntries.Where(m => m.UserId == uid).OrderByDescending(m => m.CreatedAt).Select(m => (DateTime?)m.CreatedAt).FirstOrDefault(),
                AverageMoodThisWeek = _db.MoodEntries.Where(m => m.UserId == uid && m.CreatedAt >= DateTime.UtcNow.AddDays(-7)).Select(m => (double?)m.MoodScore).Average()
            };

            return View(vm);
        }
    }
}
