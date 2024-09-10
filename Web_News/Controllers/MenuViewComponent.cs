using Microsoft.AspNetCore.Mvc;
using Web_News.Areas.Admin.ServiceAd.CategorySV;

namespace Web_News.Controllers
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public MenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetCategoriessAsync(); // Lấy danh sách categories từ service
            return View(categories); // Trả về view với model là danh sách categories
        }
    }
}
