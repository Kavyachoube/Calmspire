using CalmSpire.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CalmSpire.Controllers
{
    public class JournalController : Controller
    {
        private readonly NewsApiService _newsApiService;

        public JournalController(NewsApiService newsApiService)
        {
            _newsApiService = newsApiService;
        }

        public async Task<IActionResult> Index(string topic = "Mental Health")
        {
            var articles = await _newsApiService.GetArticlesByTopicAsync(topic);
            return View(articles); // View ko List<Article> milegi
        }
    }
}
