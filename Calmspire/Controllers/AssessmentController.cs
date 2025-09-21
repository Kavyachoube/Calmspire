using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using CalmSpire.Data;
using CalmSpire.Models;
using CalmSpire.Models.ViewModels;

namespace CalmSpire.Controllers
{
    public class AssessmentController : Controller
    {
        private readonly CalmSpireDbContext _db;
        public AssessmentController(CalmSpireDbContext db) { _db = db; }

        // List available assessments
        public IActionResult Index()
        {
            var items = _db.Assessments.Where(a => a.IsActive).OrderBy(a => a.Title).ToList();
            return View(items);
        }

        // Start page
        public IActionResult Start(int id)
        {
            var assessment = _db.Assessments.Find(id);
            if (assessment == null) return NotFound();
            return View(assessment);
        }

        // Render questions page
        public IActionResult Questions(int id)
        {
            var assessment = _db.Assessments.Find(id);
            if (assessment == null) return NotFound();

            var questions = JsonSerializer.Deserialize<List<QuestionDto>>(assessment.QuestionsJson) ?? new();
            var vm = new AssessmentTakeViewModel
            {
                AssessmentId = assessment.Id,
                Title = assessment.Title,
                Description = assessment.Description,
                Questions = questions
            };

            return View(vm);
        }

        // Handle submission
        [HttpPost]
        public IActionResult Submit()
        {
            // We will read assessment id from form
            if (!int.TryParse(Request.Form["AssessmentId"], out var assessmentId))
                return BadRequest("Missing assessment id");

            var assessment = _db.Assessments.Find(assessmentId);
            if (assessment == null) return NotFound();

            var questions = JsonSerializer.Deserialize<List<QuestionDto>>(assessment.QuestionsJson) ?? new();
            var responses = new List<object>();
            int totalScore = 0;

            for (int i = 0; i < questions.Count; i++)
            {
                var key = $"q_{i}";
                var answer = Request.Form[key].FirstOrDefault() ?? "";
                responses.Add(new { Question = questions[i].Question, Answer = answer });

                if (questions[i].Options != null && questions[i].Options.Count > 0)
                {
                    var idx = questions[i].Options.IndexOf(answer);
                    if (idx >= 0) totalScore += (idx + 1);
                }
            }

            var interpretation = totalScore switch
            {
                >= 20 => "High — consider professional support if you feel overwhelmed.",
                >= 10 => "Moderate — try daily coping strategies and check in with friends/family.",
                _ => "Low — you're doing well; continue positive habits."
            };

            var result = new AssessmentResult
            {
                UserId = HttpContext.Session.GetInt32("UserId") ?? 0,
                AssessmentId = assessment.Id,
                ResponsesJson = JsonSerializer.Serialize(responses),
                Score = totalScore,
                Interpretation = interpretation,
                CompletedAt = DateTime.UtcNow
            };

            _db.AssessmentResults.Add(result);
            _db.SaveChanges();

            return RedirectToAction("Result", new { id = result.Id });
        }

        // show result + recent history for this assessment & user
        public IActionResult Result(int id)
        {
            var res = _db.AssessmentResults.Find(id);
            if (res == null) return NotFound();

            var uid = HttpContext.Session.GetInt32("UserId") ?? 0;
            var history = _db.AssessmentResults
                .Where(r => r.UserId == uid && r.AssessmentId == res.AssessmentId)
                .OrderByDescending(r => r.CompletedAt)
                .Take(5)
                .ToList();

            ViewBag.History = history;
            // load assessment title for display
            res.Assessment = _db.Assessments.Find(res.AssessmentId)!;

            return View(res);
        }
    }
}
