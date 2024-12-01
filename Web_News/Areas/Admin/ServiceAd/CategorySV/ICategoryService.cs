using Web_News.Models;

namespace Web_News.Areas.Admin.ServiceAd.CategorySV
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        Task<IEnumerable<Category>> GetParentCategoriesAsync();
        Task<List<Category>> GetCategoriessAsync();
    }
}
