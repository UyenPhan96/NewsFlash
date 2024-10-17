using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_News.Areas.Admin.ServiceAd.DashboardSV;

namespace Web_News.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Reporter,Editor")]
    [Area("Admin")]
    public class HomeAdminController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public HomeAdminController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public IActionResult HomePage()
        {
            // Số lượng người dùng
            var numberOfUsers = _dashboardService.GetNumberOfUsers();
            var previousUserCount = _dashboardService.GetUserCountLastWeek();
            var userPercentageChange = _dashboardService.GetPercentageChange(numberOfUsers, previousUserCount);
            // Số lượng bài viết
            var numberOfDisplayedNews = _dashboardService.GetNumberOfDisplayedNews();
            var previousDisplayedNewsCount = _dashboardService.GetDisplayedNewsCountLastWeek();
            var newsPercentageChange = _dashboardService.GetPercentageChange(numberOfDisplayedNews, previousDisplayedNewsCount);
            // Số lượng quảng cáo
            var numberOfDisplayedAds = _dashboardService.GetNumberOfDisplayedAds();
            var previousDisplayedAdsCount = _dashboardService.GetDisplayedAdsCountLastWeek();
            var adsPercentageChange = _dashboardService.GetPercentageChange(numberOfDisplayedAds, previousDisplayedAdsCount);
            // Số lượng bài viết trong 10 ngày theo biết đồ
            var newsCountByDay = _dashboardService.GetNewsCountForLast7Days();
            // Số lượng quảng cáo trong 10 ngày theo biết đồ
            var adsCountByDay = _dashboardService.GetAdsCountForLast10Days();

            ViewBag.NumberOfUsers = numberOfUsers;
            ViewBag.NumberOfDisplayedNews = numberOfDisplayedNews;
            ViewBag.NumberOfDisplayedAds = numberOfDisplayedAds;
            ViewBag.UserPercentageChange = userPercentageChange;
            ViewBag.NewsPercentageChange = newsPercentageChange;
            ViewBag.AdsPercentageChange = adsPercentageChange;
            ViewBag.NewsCountByDay = newsCountByDay;
            ViewBag.AdsCountByDay = adsCountByDay;
            var newsByUserForCurrentMonth = _dashboardService.GetNewsCountByAccountForCurrentMonth();
            ViewBag.NewsByUserForCurrentMonth = newsByUserForCurrentMonth;
            return View();
        }

    }
}
