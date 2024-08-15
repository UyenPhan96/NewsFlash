using System.ComponentModel.DataAnnotations;

namespace Web_News.ViewModels
{
    public class ResetPasswordViewModel
    {


        [Required(ErrorMessage = "Nhập Mã Xác Nhận")]
        [Display(Name = "Mã xác nhận")]
        public string ResetCode { get; set; }

        [Required(ErrorMessage = "Nhập vào Mật Khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmNewPassword { get; set; }
    }
}
