using Calmspire.Models;
using CalmSpire.Data;
using CalmSpire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CalmSpire.Controllers
{
    public class AssessmentController : Controller
    {
        private readonly CalmSpireDbContext _context;

        public AssessmentController(CalmSpireDbContext context)
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

            var assessments = await _context.Assessments
                .Where(a => a.IsActive)
                .ToListAsync();

            return View(assessments);
        }

        [HttpGet]
        public async Task<IActionResult> Take(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var assessment = await _context.Assessments.FindAsync(id);
            if (assessment == null || !assessment.IsActive)
            {
                return NotFound();
            }

            return View(assessment);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(int id, Dictionary<string, string> responses)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var assessment = await _context.Assessments.FindAsync(id);
            if (assessment == null || !assessment.IsActive)
            {
                return NotFound();
            }

            var responsesJson = JsonConvert.SerializeObject(responses);

            // Simple scoring logic - count positive responses
            var score = responses.Values.Count(r => r.Contains("yes") || r.Contains("often") || r.Contains("always"));

            var interpretation = score switch
            {
                <= 2 => "Low concern - You seem to be managing well.",
                <= 5 => "Moderate concern - Consider implementing some stress management techniques.",
                _ => "Higher concern - Consider speaking with a mental health professional."
            };

            var result = new AssessmentResult
            {
                UserId = userId.Value,
                AssessmentId = id,
                ResponsesJson = responsesJson,
                Score = score,
                Interpretation = interpretation,
                CompletedAt = DateTime.UtcNow
            };

            _context.AssessmentResults.Add(result);
            await _context.SaveChangesAsync();

            return RedirectToAction("Result", new { id = result.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Result(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _context.AssessmentResults
                .Include(ar => ar.Assessment)
                .FirstOrDefaultAsync(ar => ar.Id == id && ar.UserId == userId);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var results = await _context.AssessmentResults
                .Include(ar => ar.Assessment)
                .Where(ar => ar.UserId == userId)
                .OrderByDescending(ar => ar.CompletedAt)
                .ToListAsync();

            return View(results);
        }
    }
}