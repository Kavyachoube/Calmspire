using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace CalmSpire.Services
{
    public class AIChatService
    {
        private readonly HttpClient _http;
        private readonly string? _apiKey;
        public AIChatService(HttpClient http, IConfiguration config) { _http = http; _apiKey = config["Gemini:ApiKey"]; }

        public async Task<string> GetAIResponseAsync(string userMessage)
        {
            if (string.IsNullOrWhiteSpace(_apiKey))
                return GetCannedReply(userMessage);

            var body = new { prompt = userMessage, maxOutputTokens = 256 };
            var json = JsonSerializer.Serialize(body);
            var req = new HttpRequestMessage(HttpMethod.Post, $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}")
            { Content = new StringContent(json, Encoding.UTF8, "application/json") };

            var res = await _http.SendAsync(req);
            if (!res.IsSuccessStatusCode) return GetCannedReply(userMessage);

            var resJson = await res.Content.ReadAsStringAsync();
            try
            {
                using var doc = JsonDocument.Parse(resJson);
                // tolerant parsing:
                if (doc.RootElement.TryGetProperty("candidates", out var cands) && cands.GetArrayLength() > 0)
                {
                    var first = cands[0];
                    if (first.TryGetProperty("content", out var content) && content.TryGetProperty("parts", out var parts) && parts.GetArrayLength() > 0)
                    {
                        var text = parts[0].GetProperty("text").GetString();
                        if (!string.IsNullOrWhiteSpace(text)) return text;
                    }
                }
            }
            catch { }
            return GetCannedReply(userMessage);
        }

        private string GetCannedReply(string userMessage)
        {
            var l = (userMessage ?? "").ToLowerInvariant();
            if (l.Contains("sad") || l.Contains("anx") || l.Contains("stress")) return "I hear you. That sounds tough. Try slow deep breaths. What's one small positive thing today?";
            var arr = new[] {
                "Thanks for sharing. How does that make you feel right now?",
                "That sounds important — I'm here to listen. Want to tell me more?",
                "You're doing a good job checking in with yourself. What would be one small step you could take today?"
            };
            return arr[new Random().Next(arr.Length)];
        }
    }
}
