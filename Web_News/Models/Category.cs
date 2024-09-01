using System.ComponentModel.DataAnnotations;

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

    
        public ICollection<NewsCategory> NewsCategories { get; set; }
    }
}
