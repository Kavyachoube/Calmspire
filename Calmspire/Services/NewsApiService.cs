using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using CalmSpire.Models;

namespace CalmSpire.Services
{
    public class NewsApiService
    {
        private readonly HttpClient _http;

        public NewsApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Article>> GetMentalHealthArticlesAsync()
        {
            var response = await _http.GetAsync("https://newsapi.org/v2/everything?q=mental+health&apiKey=YOUR_API_KEY");
            if (!response.IsSuccessStatusCode)
            {
                return new List<Article>(); // fallback
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            var json = await JsonDocument.ParseAsync(stream);

            var articles = new List<Article>();
            foreach (var item in json.RootElement.GetProperty("articles").EnumerateArray())
            {
                articles.Add(new Article
                {
                    Title = item.GetProperty("title").GetString() ?? "",
                    Description = item.GetProperty("description").GetString() ?? "",
                    Url = item.GetProperty("url").GetString() ?? "#",
                    UrlToImage = item.TryGetProperty("urlToImage", out var img) ? img.GetString() : null
                });
            }

            return articles;
        }
    }
}
