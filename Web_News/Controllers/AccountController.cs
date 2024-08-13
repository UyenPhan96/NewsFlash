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

        // POST: Account/Login
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

                // Đăng nhập thất bại
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
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

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kiểm tra xem email có hợp lệ không
            if (!IsValidEmail(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Invalid email format.");
                return View(model);
            }

            var user = new User
            {
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                Phone = model.Phone,
                UserName = model.Username,
                Password = model.Password,
                RegistrationDate = DateTime.Now
            };

            var success = await _accountSV.RegisterAsync(user, "User");

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Username already exists.");
                return View(model);
            }

            return RedirectToAction("Login");
        }


        // Hỗ trợ kiểm tra định dạng email
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

    }
}
