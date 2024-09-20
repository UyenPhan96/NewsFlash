using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_News.Models
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public string Content { get; set; }

        public string? Image { get; set; }

        // Foreign Key: User
        public int CreatedByUserId { get; set; }

        [ForeignKey("CreatedByUserId")]
        public User CreatedByUser { get; set; }

        [Required]
        public bool Status { get; set; }

        [EnumDataType(typeof(ApprovalStatus))]
        public ApprovalStatus ApprovalStatus { get; set; } 

        // Lý do từ chối nếu bài viết bị từ chối
        public string? RejectionReason { get; set; }

        // Mối quan hệ nhiều-nhiều với Category thông qua bảng trung gian NewsCategory
        public ICollection<NewsCategory> NewsCategories { get; set; }

    }
}
