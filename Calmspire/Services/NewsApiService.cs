using Newtonsoft.Json.Linq;

namespace CalmSpire.Services
{
    public class NewsApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "2b96209a98aa827160881f0399b71f9b";
        // 👆 यहाँ अपनी gnews.io API key डालना है (appsettings.json से भी ले सकते हो)

        public NewsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Article>> GetArticlesByTopicAsync(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
                topic = "mental health";

            var url = $"https://gnews.io/api/v4/search?q={Uri.EscapeDataString(topic)}&lang=en&token={ApiKey}";
            var response = await _httpClient.GetStringAsync(url);

            var json = JObject.Parse(response);

            var articles = json["articles"]
                .Select(a => new Article
                {
                    Title = (string)a["title"],
                    Description = (string)a["description"],
                    Url = (string)a["url"],
                    UrlToImage = (string)a["image"]
                }).ToList();

            return articles;
        }
    }

    public class Article
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = "#";
        public string? UrlToImage { get; set; }
    }
}
