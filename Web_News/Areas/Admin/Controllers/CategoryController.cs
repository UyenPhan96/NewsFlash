using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_News.Areas.Admin.ServiceAd.CategorySV;
using Web_News.Models;

namespace Web_News.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Create()
        {
            // Lấy danh sách chuyên mục mẹ từ service để hiển thị trong dropdown
            var parentCategories = _categoryService.GetParentCategoriesAsync().Result;
            ViewData["ParentCategories"] = new SelectList(parentCategories, "CategoryId", "NameCategory");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                // Thêm chuyên mục mới khi dữ liệu hợp lệ
                await _categoryService.AddCategoryAsync(category);
                TempData["success"] = "Thêm danh mục thành công";
                return RedirectToAction(nameof(GetAllCategory));
            }
            
            // Nếu ModelState không hợp lệ, trả về form với dữ liệu đã nhập để người dùng có thể sửa
            var parentCategories = await _categoryService.GetParentCategoriesAsync();
            ViewData["ParentCategories"] = new SelectList(parentCategories, "CategoryId", "NameCategory");
           
            return View(category);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            // Lấy danh sách chuyên mục mẹ và hiển thị trong dropdown
            var parentCategories = await _categoryService.GetParentCategoriesAsync();
            ViewData["ParentCategories"] = new SelectList(parentCategories, "CategoryId", "NameCategory", category.ParentCategoryId);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
           

            if (!ModelState.IsValid)
            {
                try
                {
                    // Cập nhật chuyên mục
                    await _categoryService.UpdateCategoryAsync(category);
                    TempData["success"] = "Cập nhật danh mục thành công";
                    return RedirectToAction(nameof(GetAllCategory));
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ (nếu có)
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật chuyên mục: " + ex.Message);
                }
            }

            // Nếu dữ liệu không hợp lệ, trả về trang Edit với dữ liệu và thông báo lỗi
            var parentCategories = await _categoryService.GetParentCategoriesAsync();
            ViewData["ParentCategories"] = new SelectList(parentCategories, "CategoryId", "NameCategory", category.ParentCategoryId);

            return View(category);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            TempData["success"] = $"Xóa danh mục  thành công!";
            return RedirectToAction(nameof(GetAllCategory));
        }
    }
}
