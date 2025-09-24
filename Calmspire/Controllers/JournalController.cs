using CalmSpire.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalmSpire.Controllers
{
    public class JournalController : Controller
    {
        private readonly NewsApiService _newsApi;

        public JournalController(NewsApiService newsApi)
        {
            _newsApi = newsApi;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _newsApi.GetMentalHealthArticlesAsync();
            return View(articles);
        }
    }
}
