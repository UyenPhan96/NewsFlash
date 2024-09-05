using System.ComponentModel.DataAnnotations;

namespace Web_News.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email hoặc mật khẩu không chính xác. Vui lòng nhập lại.")]
        [Display(Name = "Username or Email")]
        public string UsernameOrEmail { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
