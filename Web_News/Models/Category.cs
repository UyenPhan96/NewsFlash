using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_News.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [MaxLength(100)]
        public string NameCategory { get; set; }

        public string Describe { get; set; }

        // Thêm thuộc tính để lưu mã của chuyên mục mẹ
        public int? ParentCategoryId { get; set; }

        // Thiết lập quan hệ với chuyên mục mẹ
        [ForeignKey("ParentCategoryId")]
        public virtual Category ParentCategory { get; set; }

        // Thiết lập quan hệ với danh sách chuyên mục con
        public virtual ICollection<Category> SubCategories { get; set; }

        public ICollection<NewsCategory> NewsCategories { get; set; }
    }
}
