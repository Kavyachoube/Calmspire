using CalmSpire.Models;
using Newtonsoft.Json.Linq;

namespace CalmSpire.Services
{
    public class NewsApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "6c2f94a03b6d4504b2d110cb26491a67"; // apna key daalna

        public NewsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Article>> GetMentalHealthArticlesAsync()
        {
            var url = $"https://newsapi.org/v2/everything?q=mental%20health&language=en&sortBy=publishedAt&apiKey={ApiKey}";
            var response = await _httpClient.GetStringAsync(url);

            var json = JObject.Parse(response);
            var articles = json["articles"]
                .Select(a => new Article
                {
                    Title = (string)a["title"],
                    Description = (string)a["description"],
                    Url = (string)a["url"],
                    UrlToImage = (string)a["urlToImage"]
                }).ToList();

            return articles;
        }
    }
}
