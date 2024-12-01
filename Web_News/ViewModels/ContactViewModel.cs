using System.ComponentModel.DataAnnotations;

namespace Web_News.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Tên người liên hệ không được quá 100 ký tự.")]
        public string ContactName { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public IFormFile MediaFile { get; set; } // Đổi tên từ ImageBN thành MediaFile để hỗ trợ cả ảnh và video

        public string MediaFilePath { get; set; } // Để lưu đường dẫn đến tệp đã lưu

        [Url]
        public string Link { get; set; }
    }
}
