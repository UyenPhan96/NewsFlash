using System.ComponentModel.DataAnnotations;

namespace Web_News.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Nhập vào Tài khoản hoặc Email")]
        [Display(Name = "Username or Email")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Nhập vào Mật Khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
