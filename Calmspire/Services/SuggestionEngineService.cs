using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CalmSpire.Data;
using System.Linq;

namespace CalmSpire.Services
{
    public class SuggestionEngineService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CalmSpireDbContext _db;

        public SuggestionEngineService(IHttpClientFactory httpClientFactory, CalmSpireDbContext db)
        {
            _httpClientFactory = httpClientFactory;
            _db = db;
        }

        public async Task<string> GetSuggestionForUserAsync(int userId)
        {
            // 1. Last mood entry check karo
            var mood = _db.MoodEntries
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.EntryDate)
                .FirstOrDefault();

            string moodBasedPrompt = "Stay positive and enjoy your day!";
            if (mood != null)
            {
                if (mood.MoodScore <= 2)
                    moodBasedPrompt = "It seems you're feeling low 💙. Even the darkest night will end and the sun will rise.";
                else if (mood.MoodScore == 3)
                    moodBasedPrompt = "A balanced day 🌿 Take a deep breath and practice gratitude.";
                else
                    moodBasedPrompt = "You're shining bright ☀️ Keep spreading positivity!";
            }

            // 2. Quote API call
            var client = _httpClientFactory.CreateClient();
            string quote = "";
            try
            {
                var response = await client.GetAsync("https://zenquotes.io/api/random");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(json);
                    quote = doc.RootElement[0].GetProperty("q").GetString()
                          + " — " + doc.RootElement[0].GetProperty("a").GetString();
                }
            }
            catch
            {
                quote = "Keep going, you're doing better than you think 💪";
            }

            // 3. Combine suggestion
            return $"{moodBasedPrompt}\n\n💡 Motivation: {quote}";
        }
    }
}
