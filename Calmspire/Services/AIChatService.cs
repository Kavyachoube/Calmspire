using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace CalmSpire.Services
{
    public class AIChatService
    {
        private readonly HttpClient _http;
        private readonly string? _apiKey;

        public AIChatService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["AIzaSyD5OjQPorklHttVObVVGnd4ydbMAjhb6EY"];
        }

        // Public method used by controller to get reply
        public async Task<string> GetAIResponseAsync(string userMessage)
        {
            // If no API key, return a canned friendly reply (development fallback)
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                return GetCannedReply(userMessage);
            }

            // Build request body per Gemini generateContent endpoint
            var body = new
            {
                // This shape may change with API; keep generic minimal
                prompt = userMessage,
                maxOutputTokens = 256
            };

            var json = JsonSerializer.Serialize(body);
            var req = new HttpRequestMessage(HttpMethod.Post,
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var res = await _http.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                // safe fallback if API errors
                return "Sorry — I couldn't reach my assistant right now. Please try again in a moment.";
            }

            var resJson = await res.Content.ReadAsStringAsync();

            try
            {
                using var doc = JsonDocument.Parse(resJson);
                // attempt to read common fields — tolerant parsing
                if (doc.RootElement.TryGetProperty("candidates", out var candidates) &&
                    candidates.GetArrayLength() > 0)
                {
                    var first = candidates[0];
                    if (first.TryGetProperty("content", out var content) &&
                        content.TryGetProperty("parts", out var parts) &&
                        parts.GetArrayLength() > 0)
                    {
                        var text = parts[0].GetProperty("text").GetString();
                        if (!string.IsNullOrWhiteSpace(text)) return text;
                    }
                }

                // If response shape differs, best-effort traverse
                if (doc.RootElement.TryGetProperty("output", out var outEl) &&
                    outEl.TryGetProperty("content", out var cont) &&
                    cont.GetArrayLength() > 0)
                {
                    return cont[0].GetProperty("text").GetString() ?? GetCannedReply(userMessage);
                }
            }
            catch
            {
                // Ignore parse exception and fallback
            }

            return GetCannedReply(userMessage);
        }

        private string GetCannedReply(string userMessage)
        {
            // Very small rule-based responses to keep tone friendly & safe
            var lower = userMessage?.ToLowerInvariant() ?? "";
            if (lower.Contains("anx") || lower.Contains("sad") || lower.Contains("stres"))
            {
                return "I hear you. That sounds tough. Try a few slow deep breaths and tell me one small thing that felt a bit better today.";
            }

            var defaultReplies = new[]
            {
                "Thanks for sharing. How does that make you feel right now?",
                "That sounds important — I'm here to listen. Want to tell me more?",
                "You're doing a good job checking in with yourself. What would be one small step you could take today?"
            };

            return defaultReplies[new Random().Next(defaultReplies.Length)];
        }
    }
}
