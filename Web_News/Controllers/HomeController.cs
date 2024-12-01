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

            // Lấy 6 bài theo 6 thể loại khác nhau
            var worldNews = await _newsService.GetNewsByCategory(53, 1); 
            var cultureNews = await _newsService.GetNewsByCategory(54, 1); 
            var lawNews = await _newsService.GetNewsByCategory(55, 1); 
            var sportsNews = await _newsService.GetNewsByCategory(59, 1); 
            var trafficNews = await _newsService.GetNewsByCategory(62, 1); 
            var lifeNews = await _newsService.GetNewsByCategory(57, 1);
            // lấy tin nhanh 360
            var news360 = await _newsService.GetTopNews360(7);
            // lấy 6 tin thế giới
            var get6WorldNews = await _newsService.GetNewsByCategory(53, 6);
            // Lấy 6 bài Kinh tế 
            var get6economyNews = await _newsService.GetNewsByCategory(52, 6);
            // Lấy 6 bài công nghệ 
            var get6technologyNews = await _newsService.GetNewsByCategory(61, 6);
            // Lấy 6 bài văn hóa
            var get6cultureNews = await _newsService.GetNewsByCategory(54, 6);
            // Lấy 6 bài thể thao
            var get6sportsNews = await _newsService.GetNewsByCategory(59, 6);
            // Lấy 6 bài pháp luật
            var get6lawNews = await _newsService.GetNewsByCategory(55, 6); 
            // Lấy 6 bài giao thông
            var get6trafficNews = await _newsService.GetNewsByCategory(62, 6); 


            // thêm bài phần Quan tâm
            var recommendedNews = new List<News>();
            recommendedNews.AddRange(worldNews);
            recommendedNews.AddRange(cultureNews);
            recommendedNews.AddRange(lawNews);
            recommendedNews.AddRange(sportsNews);
            recommendedNews.AddRange(trafficNews);
            recommendedNews.AddRange(lifeNews);

            var viewModel = new ListViewModel
            {
                LatestNews = latestNews,
                News360 = news360, 
                SidebarAdvertisements = sidebarAds,
                HeaderAdvertisements = headerAds,
                FooterAdvertisements = footerAds,
                RecommendedNews = recommendedNews,
                WorldNews = get6WorldNews,
                CultureNews = get6cultureNews,
                LawNews = get6lawNews,
                SportsNews = get6sportsNews,
                TrafficNews = get6trafficNews,
                EconomyNews = get6economyNews,
                TechnologyNews = get6technologyNews
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
