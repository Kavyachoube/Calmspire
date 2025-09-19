using CalmSpire.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalmSpire.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var chatHistory = await _chatService.GetChatHistoryAsync(userId.Value);
            return View(chatHistory);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Message cannot be empty");
            }

            var response = await _chatService.GetResponseAsync(request.Message);
            var chatMessage = await _chatService.SaveChatMessageAsync(userId.Value, request.Message, response);

            return Json(new
            {
                id = chatMessage.Id,
                message = chatMessage.Message,
                response = chatMessage.Response,
                createdAt = chatMessage.CreatedAt
            });
        }

        public class ChatRequest
        {
            public string Message { get; set; } = string.Empty;
        }
    }
}