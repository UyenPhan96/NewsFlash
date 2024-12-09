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



        // GET: News/Details/5
        [Authorize(Roles = "Admin,Reporter,Editor")]
        public async Task<IActionResult> Details(int id, string returnUrl)
        {
            var newsItem = await _newsService.GetNewsByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(newsItem);
        }



        // GET: News/Create
        [Authorize(Roles = "Admin,Reporter,Editor")]
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
        [Authorize(Roles = "Admin,Reporter,Editor")]
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
                    TempData["success"] = "Bài viết đã được tạo thành công";
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




        /// <summary>
        /// GET: News/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Reporter,Editor")]
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
        [Authorize(Roles = "Admin,Reporter,Editor")]
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
                    TempData["success"] = "Sửa bài viết thành công";
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



        /// <summary>
        /// GET: News/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Reporter,Editor")]
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

      
        /// <summary>
        /// POST: News/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Reporter,Editor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _newsService.DeleteNewsAsync(id);
                TempData["success"] = "Xóa bài viết thành công";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                TempData["error"] = "Có lỗi xảy ra"; 
                ModelState.AddModelError("", ex.Message);
            }

            // Nếu có lỗi, lấy lại đối tượng News để hiển thị thông báo lỗi
            var news = await _newsService.GetNewsByIdAsync(id);
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(int[] selectedIds)
        {
            if (selectedIds == null || selectedIds.Length == 0)
            {
                return RedirectToAction("AllNews");
            }

            foreach (var id in selectedIds)
            {
                await _newsService.DeleteNewsAsync(id);
            }
            TempData["success"] = "Xóa bài viết thành công!";
            return RedirectToAction("AllNews");
        }


        [Authorize(Roles = "Admin,Reporter,Editor")]
        public async Task<IActionResult> Index(ApprovalStatus? statusFilter = ApprovalStatus.Pending, int pageNumber = 1, string titleSearchTerm = null, DateTime? publishDate = null, string authorSearchTerm = null, int? categoryId = null)
        {
            int userId;
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
            {
                const int pageSize = 10; // Số lượng bài viết mỗi trang
                var pagedResult = await _newsService.GetNewsByApprovalStatusAsync(statusFilter, userId, pageNumber, pageSize, titleSearchTerm, publishDate, authorSearchTerm, categoryId);

                ViewData["StatusFilter"] = statusFilter;
                ViewData["TotalPages"] = pagedResult.TotalPages;
                ViewData["CurrentPage"] = pageNumber;
                ViewData["TitleSearchTerm"] = titleSearchTerm; // Tìm kiếm theo tiêu đề
                ViewData["PublishDate"] = publishDate; // Tìm kiếm theo ngày viết
                ViewData["AuthorSearchTerm"] = authorSearchTerm; // Tìm kiếm theo người viết
                ViewData["CategoryId"] = categoryId; // Tìm kiếm theo thể loại

                return View(pagedResult.News);
            }
            else
            {
                return BadRequest("Invalid user ID");
            }
        }



        // GET: News/FilterByStatus
        [Authorize(Roles = "Admin,Reporter,Editor")]
        public IActionResult FilterByStatus(ApprovalStatus status)
        {
            return RedirectToAction("Index", new { statusFilter = status });
        }


        /// <summary>
        /// Hiển thị danh sách các bài viết đang chờ duyệt cho Editor
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> PendingNews(string status)
        {
            var pendingNews = await _newsService.GetAllPendingNewsAsync();
            if (status == "Pending")
            {
                // Thực hiện lọc theo trạng thái nếu cần
            }
            return View(pendingNews);
        }

        /// <summary>
        /// Duyệt bài viết giành cho Editor
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> ApproveNews(int newsId)
        {
            var result = await _newsService.ApproveNewsAsync(newsId);
            if (!result)
            {
                return BadRequest("Không thể duyệt bài viết này.");
            }
            TempData["success"] = "Duyệt bài viết thành công";
            return RedirectToAction("PendingNews");
        }

        /// <summary>
        /// Từ chối bài viết giành cho Editor
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="rejectionReason"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Chi tiết bài viết đang chờ duyệt giành cho Editor
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> NewsDetail(int newsId)
        {
            var newsDetail = await _newsService.GetNewsByIdAsync(newsId);
            if (newsDetail == null)
            {
                return NotFound();
            }
            return View(newsDetail);
        }


        /// <summary>
        /// Hiển thi danh sách tất cả bài viết đã duyệt || tìm kiếm || phân trang
        /// </summary>
        /// <param name="status"></param>
        /// <param name="pageNumber"></param>
        /// <param name="titleSearchTerm"></param>
        /// <param name="publishDate"></param>
        /// <param name="authorSearchTerm"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AllNews
            (ApprovalStatus? status = ApprovalStatus.Approved, int pageNumber = 1,
            string titleSearchTerm = null,
            DateTime? publishDate = null,
            string authorSearchTerm = null,
            int? categoryId = null)
        {
            const int pageSize = 10; // Số bài viết mỗi trang
            var pagedResult = await _newsService.GetPagedNewsByApprovalStatusAsync(status, pageNumber, pageSize, titleSearchTerm, publishDate, authorSearchTerm, categoryId);
            ViewData["StatusF"] = status;
            ViewData["TotalPages"] = pagedResult.TotalPages;
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TitleSearchTerm"] = titleSearchTerm; // Tìm kiếm theo tiêu đề
            ViewData["PublishDate"] = publishDate; // Tìm kiếm theo ngày viết
            ViewData["AuthorSearchTerm"] = authorSearchTerm; // Tìm kiếm theo người viết
            ViewData["CategoryId"] = categoryId; // Tìm kiếm theo thể loại
            return View(pagedResult.News);
        }



        /// <summary>
        /// Cập nhật , thu hồi lại trạng thái bài viết
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var result = await _newsService.UpdateNewsStatusAsync(id, ApprovalStatus.Rejected, false);

            if (!result)
            {
                return NotFound(); // Nếu không tìm thấy bài viết
            }

            return RedirectToAction("AllNews", new { status = ApprovalStatus.Rejected });
        }


    }
}
