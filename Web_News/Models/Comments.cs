using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_News.Models
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Foreign Key: User (Người bình luận)
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        // Foreign Key: News (Bài viết được bình luận)
        [Required]
        public int NewsId { get; set; }

        [ForeignKey("NewsId")]
        public News News { get; set; }

        // Foreign Key: Parent Comment (Phản hồi đến một bình luận khác)
        public int? ParentCommentId { get; set; }

        [ForeignKey("ParentCommentId")]
        public Comments ParentComment { get; set; }

        // Các bình luận con
        public ICollection<Comments> Replies { get; set; }
    }
}
