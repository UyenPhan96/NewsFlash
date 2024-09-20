using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_News.Areas.Admin.ServiceAd.CategorySV;
using Web_News.Areas.Admin.ServiceAd.NewsSV;
using Web_News.Areas.Admin.ViewModels;
using Web_News.Models;

namespace Web_News.Areas.Admin.Controllers
{
   
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
        [Authorize(Roles = "Admin,Reporter,Editor")]
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


        [Authorize(Roles = "Admin,Reporter,Editor")]
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
        [Authorize(Roles = "Admin,Reporter,Editor")]
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
                        //Status = model.Status
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
        [Authorize(Roles = "Admin,Reporter,Editor")]
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
        [Authorize(Roles = "Admin,Reporter,Editor")]
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
                        Status = model.Status,
                        ApprovalStatus = ApprovalStatus.Pending
                    };
         
                    await _newsService.UpdateNewsAsync(news, model.SelectedCategories);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the news.");
                }
            }

            model.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(model);
        }


        [Authorize(Roles = "Admin,Reporter,Editor")]
        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null) return NotFound();
            var isReporter = User.IsInRole("Reporter");

            if (isReporter && news.Status == true)
            {
                return Forbid(); // Hoặc chuyển hướng đến một trang thông báo lỗi nếu cần
            }
            // Trả về mô hình News thay vì NewsViewModel
            return View(news);
        }

        [Authorize(Roles = "Admin,Reporter,Editor")]
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


        [Authorize(Roles = "Admin,Reporter")]
        public async Task<IActionResult> Index(ApprovalStatus? statusFilter = ApprovalStatus.Pending)
        {
            int userId;
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId)) // Giả sử bạn lưu user ID trong claim
            {
                var newsList = await _newsService.GetNewsByApprovalStatusAsync(statusFilter, userId);
                ViewData["StatusFilter"] = statusFilter;
                return View(newsList);
            }
            else
            {
                // Xử lý trường hợp không thể chuyển đổi userId
                return BadRequest("Invalid user ID");
            }
        }

        [Authorize(Roles = "Admin,Reporter")]
        // GET: News/FilterByStatus
        public IActionResult FilterByStatus(ApprovalStatus status)
        {
            return RedirectToAction("Index", new { statusFilter = status });
        }



        [Authorize(Roles = "Admin,Editor")]
        // Hiển thị danh sách các bài viết chờ duyệt
        public async Task<IActionResult> PendingNews()
        {
            var pendingNews = await _newsService.GetAllPendingNewsAsync();
            return View(pendingNews);
        }

        // Duyệt bài viết
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public async Task<IActionResult> ApproveNews(int newsId)
        {
            var result = await _newsService.ApproveNewsAsync(newsId);
            if (!result)
            {
                return BadRequest("Không thể duyệt bài viết này.");
            }
            return RedirectToAction("PendingNews");
        }

        // Từ chối bài viết nếu sử dụng 
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public async Task<IActionResult> RejectNews(int newsId, string rejectionReason)
        {
            var result = await _newsService.RejectNewsAsync(newsId, rejectionReason);
            if (!result)
            {
                return BadRequest("Không thể từ chối bài viết này.");
            }
            return RedirectToAction("PendingNews");
        }
        [Authorize(Roles = "Admin,Editor")]
        // Chi tiết bài viết đang chờ duyệt
        public async Task<IActionResult> NewsDetail(int newsId)
        {
            var newsDetail = await _newsService.GetNewsByIdAsync(newsId);
            if (newsDetail == null)
            {
                return NotFound();
            }
            return View(newsDetail);
        }
    }
}
