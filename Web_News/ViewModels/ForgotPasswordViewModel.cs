using System.ComponentModel.DataAnnotations;

namespace Web_News.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Nhập Vào Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
