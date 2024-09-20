using System.ComponentModel.DataAnnotations;
using Web_News.Models;

namespace Web_News.Areas.Admin.ViewModels
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string? Image { get; set; }
        public DateTime PublishDate { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string CreatedByUserName { get; set; }
        [Required]
        public bool Status { get; set; }
        // Thuộc tính bổ sung cho hiển thị trạng thái
        public string StatusDisplay => Status ? "On" : "Off";
     
        public List<Category> ListCategories { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<int> SelectedCategories { get; set; }
    }
}
