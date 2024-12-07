using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using Web_News.Areas.Admin.ServiceAd.CategorySV;
using Web_News.Areas.Admin.ServiceAd.NewsSV;
using Web_News.Areas.Admin.ViewModels;
using Web_News.Models;
using Web_News.ViewModels;

namespace Web_News.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;

        public NewsController(INewsService newsService,ICategoryService categoryService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int? id,int page =1) // id là CategoryId
        {
            int pageSize = 10;
            int categoryId = id ?? 0;
            var latestNews = await _newsService.GetNewsByCategoryList(categoryId, page, pageSize);
            var sidebarAds = await _newsService.GetActiveAdvertisementsAsync(BannerPosition.Sidebar, 8);
            var headerAds = await _newsService.GetActiveAdvertisementsAsync(BannerPosition.Header, 1);
            var footerAds = await _newsService.GetActiveAdvertisementsAsync(BannerPosition.Footer, 1);
            var categories = await _categoryService.GetAllCategoriesAsync();


            // Lấy danh mục được chọn dựa trên id
            var selectedCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

            // Lọc danh mục liên quan đến danh mục mẹ
            var relatedCategories = selectedCategory != null
                ? categories.Where(c => c.ParentCategoryId == selectedCategory.CategoryId 
                || c.CategoryId == selectedCategory.ParentCategoryId 
                || c.CategoryId == selectedCategory.CategoryId).ToList()
                : categories.ToList();
            var totalNewsCount = await _newsService.GetNewsCountByCategory(categoryId);

            var viewModel = new ListViewModel
            {
                LatestNews = latestNews,
                SidebarAdvertisements = sidebarAds,
                HeaderAdvertisements = headerAds,
                FooterAdvertisements = footerAds,
                Categories = relatedCategories,
                SelectedCategoryId = categoryId,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalNewsCount
            };

            return View(viewModel); 
        }
        public async Task<IActionResult> SearchNews(int? categoryId, string searchQuery, DateTime? startDate, DateTime? endDate, int pageNumber = 1, int pageSize = 10)
        {
            var categories = await _categoryService.GetCategoriessAsync();

            // Gọi Service để tìm bài viết
            var newsList = await _newsService.SearchNewsAsync(categoryId, searchQuery, startDate, endDate);

            // Tính toán tổng số trang
            var totalItems = newsList.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Phân trang
            var paginatedNewsList = newsList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Gọi Service để lấy quảng cáo
            var sidebarAds = await _newsService.GetActiveAdvertisementsAsync(BannerPosition.Sidebar, 8);
            var headerAds = await _newsService.GetActiveAdvertisementsAsync(BannerPosition.Header, 1);
            var footerAds = await _newsService.GetActiveAdvertisementsAsync(BannerPosition.Footer, 1);

            // Tạo ViewModel để truyền dữ liệu ra View
            var viewModel = new SearchWithAdsViewModel
            {
                LatestNews = paginatedNewsList,
                SidebarAdvertisements = sidebarAds,
                HeaderAdvertisements = headerAds,
                FooterAdvertisements = footerAds,
                SearchQuery = searchQuery,
                CategoryId = categoryId ?? 0,
                StartDate = startDate,
                EndDate = endDate,
                Categories = categories,
                PageNumber = pageNumber,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

    }
}
