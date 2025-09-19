using CalmSpire.Data;
using CalmSpire.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalmSpire.Controllers
{
    public class DashboardController : Controller
    {
        private readonly CalmSpireDbContext _context;

        public DashboardController(CalmSpireDbContext context)
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

            var userName = HttpContext.Session.GetString("UserName") ?? "User";

            // Get mood entries for the last 7 days
            var sevenDaysAgo = DateTime.Today.AddDays(-6);
            var recentMoods = await _context.MoodEntries
                .Where(m => m.UserId == userId && m.EntryDate >= sevenDaysAgo)
                .OrderBy(m => m.EntryDate)
                .ToListAsync();

            // Get statistics
            var totalJournalEntries = await _context.JournalEntries
                .CountAsync(j => j.UserId == userId);

            var assessmentsCompleted = await _context.AssessmentResults
                .CountAsync(ar => ar.UserId == userId);

            var lastMoodEntry = await _context.MoodEntries
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.EntryDate)
                .Select(m => m.EntryDate)
                .FirstOrDefaultAsync();

            var averageMoodThisWeek = recentMoods.Any()
                ? recentMoods.Average(m => m.MoodScore)
                : (double?)null;

            var viewModel = new DashboardViewModel
            {
                UserName = userName,
                RecentMoods = recentMoods,
                TotalJournalEntries = totalJournalEntries,
                AssessmentsCompleted = assessmentsCompleted,
                LastMoodEntry = lastMoodEntry,
                AverageMoodThisWeek = averageMoodThisWeek
            };

            return View(viewModel);
        }
    }
}