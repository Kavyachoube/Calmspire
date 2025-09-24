using CalmSpire.Data;
using CalmSpire.Models;
using CalmSpire.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalmSpire.Controllers
{
    public class ChatController : Controller
    {
        private readonly CalmSpireDbContext _db;
        private readonly AIChatService _ai;
        public ChatController(CalmSpireDbContext db, AIChatService ai) { _db = db; _ai = ai; }

        [HttpGet]
        public IActionResult Index()
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (!uid.HasValue) return RedirectToAction("Login", "Account");
            var history = _db.ChatMessages.Where(c => c.UserId == uid.Value).OrderBy(c => c.CreatedAt).Take(200).ToList();
            return View(history);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage input)
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (!uid.HasValue) return Unauthorized();
            if (input == null || string.IsNullOrWhiteSpace(input.Message)) return BadRequest("Empty");

            var userMessage = new ChatMessage { UserId = uid.Value, Sender = "user", Message = input.Message, CreatedAt = DateTime.UtcNow };
            _db.ChatMessages.Add(userMessage);
            await _db.SaveChangesAsync();

            var reply = await _ai.GetAIResponseAsync(input.Message);
            var botMessage = new ChatMessage { UserId = uid.Value, Sender = "bot", Message = reply, CreatedAt = DateTime.UtcNow };
            _db.ChatMessages.Add(botMessage);
            await _db.SaveChangesAsync();

            return Json(new { bot = reply });
        }
    }
}
