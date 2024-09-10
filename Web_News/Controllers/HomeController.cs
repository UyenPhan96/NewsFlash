using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_News.Areas.Admin.ServiceAd.NewsSV;
using Web_News.Areas.Admin.ViewModels;
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
            var sidebarAds = await _newsService.GetActiveAdvertisementsAsync(BannerPosition.Sidebar,4);
            var headerAds = await _newsService.GetActiveAdvertisementsAsync(BannerPosition.Header,1);
            var footerAds = await _newsService.GetActiveAdvertisementsAsync(BannerPosition.Footer,1);

            var viewModel = new ListViewModel
            {
                LatestNews = latestNews,
                SidebarAdvertisements = sidebarAds,
                HeaderAdvertisements = headerAds,
                FooterAdvertisements = footerAds
            };

            return View(viewModel);
        }



        public async Task<IActionResult> Privacy(int id)
        {
            var newsDetails = await _newsService.GetNewsByIdAsync(id);

            if (newsDetails == null)
            {
                return NotFound(); 
            }

            return View(newsDetails); 
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
