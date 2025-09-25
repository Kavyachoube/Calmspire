using CalmSpire.Models.ViewModels;
using CalmSpire.Services;
using CalmSpire.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CalmSpire.Controllers
{
    public class JournalController : Controller
    {
        private readonly CalmSpireDbContext _context;
        private readonly NewsApiService _newsApi;

        public JournalController(CalmSpireDbContext context, NewsApiService newsApi)
        {
            _context = context;
            _newsApi = newsApi;
        }

        public async Task<IActionResult> Index()
        {
            var model = new JournalIndexViewModel
            {
                LocalEntries = _context.JournalEntries.ToList(),
                Articles = await _newsApi.GetMentalHealthArticlesAsync()
            };

            return View(model);
        }
    }
}
