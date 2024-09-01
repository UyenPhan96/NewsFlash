using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_News.Areas.Admin.ServiceAd.CategorySV;
using Web_News.Areas.Admin.ServiceAd.NewsSV;
using Web_News.Areas.Admin.ViewModels;
using Web_News.Models;

namespace Web_News.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;
        public NewsController(INewsService newsService, ICategoryService categoryService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var newsItem = await _newsService.GetNewsByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            return View(newsItem);
        }



        // GET: News/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var viewModel = new NewsViewModel
            {
                Categories = categories
            };
            return View(viewModel);
        }

        // POST: News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    if (string.IsNullOrEmpty(userIdClaim))
                    {
                        // Xử lý trường hợp không tìm thấy UserID
                        throw new Exception("User ID claim is missing. Ensure the user is logged in correctly.");
                    }

                    var userId = int.Parse(userIdClaim);

                    var news = new News
                    {
                        Title = model.Title,
                        Content = model.Content,
                        Image = model.Image,
                        CreatedByUserId = userId,
                        Status = model.Status
                    };

                    // Lưu bản ghi News trước để có được NewsId
                    await _newsService.CreateNewsAsync(news, model.SelectedCategories);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi và hiển thị thông báo lỗi
                    ModelState.AddModelError("", "An error occurred while creating the news.");
                }
            }

            // Nếu model không hợp lệ, nạp lại danh sách thể loại
            model.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(model);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null) return NotFound();

            var categories = await _categoryService.GetAllCategoriesAsync();
            var selectedCategories = news.Categories.Select(c => c.CategoryId);

            var viewModel = new NewsViewModel
            {
                NewsId = news.NewsId,
                Title = news.Title,
                Content = news.Content,
                Image = news.Image,
                Status = news.Status,
                Categories = categories,
                SelectedCategories = selectedCategories
            };

            return View(viewModel);
        }



        // POST: News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var news = new News
                    {
                        NewsId = model.NewsId,
                        Title = model.Title,
                        Content = model.Content,
                        Image = model.Image,
                        Status = model.Status
                    };

                    await _newsService.UpdateNewsAsync(news, model.SelectedCategories);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the news.");
                }
            }

            // Re-fetch categories if the model state is not valid
            model.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(model);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null) return NotFound();

            // Trả về mô hình News thay vì NewsViewModel
            return View(news);
        }


        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _newsService.DeleteNewsAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the news.");
            }

            // Nếu có lỗi, lấy lại đối tượng News để hiển thị thông báo lỗi
            var news = await _newsService.GetNewsByIdAsync(id);
            return View(news);
        }



        // GET: News/Index
        public async Task<IActionResult> Index()
        {
            var newsList = await _newsService.GetAllNewsAsync();
            return View(newsList);
        }
    }
}
