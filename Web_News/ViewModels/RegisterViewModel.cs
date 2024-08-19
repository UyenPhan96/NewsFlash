using System.ComponentModel.DataAnnotations;

namespace Web_News.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Họ và tên ?")]
        [MaxLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nhập vào Email của bạn")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nhập vào tài khoản ")]
        [MaxLength(50, ErrorMessage = "Tài khoản  không được vượt quá 50 ký tự")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Nhập vào mật khẩu ")]
        [MaxLength(100, ErrorMessage = "Mật khẩu  không được vượt quá 50 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu ")]
        public string ConfirmPassword { get; set; }
    }
}
