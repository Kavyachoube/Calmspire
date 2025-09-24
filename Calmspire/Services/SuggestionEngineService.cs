using CalmSpire.Data;
using System.Text.Json;

namespace CalmSpire.Services
{
    public class SuggestionEngineService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly CalmSpireDbContext _db;
        public SuggestionEngineService(IHttpClientFactory httpFactory, CalmSpireDbContext db) { _httpFactory = httpFactory; _db = db; }

        public async Task<string> GetSuggestionForUserAsync(int userId)
        {
            var mood = _db.MoodEntries.Where(m => m.UserId == userId).OrderByDescending(m => m.CreatedAt).FirstOrDefault();
            string moodBased = "Stay positive and enjoy your day!";
            if (mood != null)
            {
                if (mood.MoodScore <= 3) moodBased = "It seems you're feeling low 💙. Even the darkest night will end and the sun will rise.";
                else if (mood.MoodScore <= 6) moodBased = "A balanced day 🌿 Take a deep breath and practice gratitude.";
                else moodBased = "You're shining bright ☀️ Keep spreading positivity!";
            }

            var client = _httpFactory.CreateClient();
            string quote = "Keep going, you're doing better than you think 💪";
            try
            {
                var res = await client.GetAsync("https://zenquotes.io/api/random");
                if (res.IsSuccessStatusCode)
                {
                    var j = await res.Content.ReadAsStringAsync();
                    using var d = JsonDocument.Parse(j);
                    var q = d.RootElement[0].GetProperty("q").GetString();
                    var a = d.RootElement[0].GetProperty("a").GetString();
                    quote = (q ?? "") + " — " + (a ?? "");
                }
            }
            catch { /* fallback */ }

            return $"{moodBased}\n\n💡 Motivation: {quote}";
        }
    }
}
