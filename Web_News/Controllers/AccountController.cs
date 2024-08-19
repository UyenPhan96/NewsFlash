using Microsoft.AspNetCore.Mvc;
using Web_News.Models;
using Web_News.Services.Account;
using Web_News.ViewModels;

namespace Web_News.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountSV;

        public AccountController(IAccountService accountSV)
        {
            _accountSV = accountSV;
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
   
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (user, roles) = await _accountSV.LoginAsync(model.UsernameOrEmail, model.Password);
                if (user != null)
                {
                    // Kiểm tra vai trò và chuyển hướng dựa trên vai trò
                    if (roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("HomePage", "HomeAdmin", new { area = "Admin" });
                    }
                }

                // Đăng nhập thất bại - Thông báo "Tài khoản hoặc mật khẩu sai"
                ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu sai.");
            }

            return View(model);
        }


        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {
            await _accountSV.Logout();
            return RedirectToAction("Login");
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
               // Kiểm tra xem định dạng email có hợp lệ không
    if (!IsValidEmail(model.Email))
    {
        ModelState.AddModelError(nameof(model.Email), "Định dạng email không hợp lệ.");
        return View(model);
    }

            // Kiểm tra xem tên người dùng đã tồn tại chưa
            if (await _accountSV.UserNameExistsAsync(model.Username))
            {
                ModelState.AddModelError(nameof(model.Username), "Tên người dùng đã tồn tại.");
                return View(model);
            }

            // Kiểm tra xem email đã tồn tại chưa
            if (await _accountSV.EmailExistsAsync(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Email đã tồn tại.");
                return View(model);
            }

            // Kiểm tra xem mật khẩu và mật khẩu xác nhận có khớp không
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(nameof(model.ConfirmPassword), "Mật khẩu không khớp.");
                return View(model);
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Username,
                Password = model.Password,
                RegistrationDate = DateTime.Now
            };

            // Đăng ký người dùng mới
            var success = await _accountSV.RegisterAsync(user, "User");

            if (!success)
            {
                ModelState.AddModelError(nameof(model.Password), "Mật khẩu phải có ít nhất 8 chữ cái, bao gồm chữ hoa, chữ thường, số");
                return View(model);
            }

            return RedirectToAction("Login");
        }



        // kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        // GET: Account/ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _accountSV.SendPasswordResetCodeAsync(model.Email);
            if (result)
            {
                // Thông báo thành công hoặc chuyển hướng đến trang khác
                return RedirectToAction("ResetPassword");
            }

            ModelState.AddModelError("", "Email không hợp lệ hoặc không tồn tại.");
            return View(model);
        }

        // GET: Account/ResetPassword
        public IActionResult ResetPassword()
        {
            return View();
        }

        // POST: Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _accountSV.ResetPasswordAsync( model.ResetCode, model.NewPassword);
            if (result)
            {
                // Thông báo thành công hoặc chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Mã xác nhận không hợp lệ hoặc đã hết hạn.");
            return View(model);
        }

    }
}
