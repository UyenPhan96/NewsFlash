using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_News.Areas.Admin.ServiceAd.NewsSV;
using Web_News.Models;

namespace Web_News.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsService _newsService;
        public HomeController(ILogger<HomeController> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            var latestNews = await _newsService.GetTop4News();

            return View(latestNews);
        }
        // Action ?? hi?n th? chi ti?t bài vi?t
        public async Task<IActionResult> Privacy(int id)
        {
            var newsDetails = await _newsService.GetNewsByIdAsync(id);

            if (newsDetails == null)
            {
                return NotFound(); // Tr? v? trang 404 n?u bài vi?t không tìm th?y
            }

            return View(newsDetails); // Tr? v? view v?i d? li?u chi ti?t bài vi?t
        }

        //[Authorize(Roles = "Admin")]
        //public IActionResult Privacy()
        //{
        //    return View();
        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
