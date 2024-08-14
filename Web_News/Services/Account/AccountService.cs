using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Web_News.Models;
using Web_News.Services.PasswordH;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Web_News.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }



        // Kiểm tra Tên đăng nhập có tồn tại chưa
        public async Task<bool> UserNameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }

        // Kiểm tra Email đã tồn tại chưa
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        // Phần Kiểm tra mật khẩu
        private bool IsValidPassword(string password)
        {
           
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$");
            return regex.IsMatch(password);
        }



        // Phần đăng Nhập
        public async Task<(User? user, List<string> roles)> LoginAsync(string usernameOrEmail, string password)
        {
            if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
            {
                return (null, new List<string>());
            }

            var user = _context.Users
                .SingleOrDefault(u => (u.UserName == usernameOrEmail || u.Email == usernameOrEmail));

            if (user != null)
            {
                    // Kiểm tra mật khẩu
                if (PasswordHasher.VerifyPassword(user.Password, password))
                {
                    // Lấy vai trò của người dùng
                    var roles = _context.UserRoles
                        .Where(ur => ur.UserId == user.UserID)
                        .Select(ur => _context.Roles.FirstOrDefault(r => r.RoleID == ur.RoleId).NameRole)
                        .ToList();

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email)
                    };
                    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Đăng nhập vào hệ thống
                    await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return (user, roles);
                }
            }

            return (null, new List<string>());
        }





        // Phần đăng Ký
        public async Task<bool> RegisterAsync(User user, string role)
        {
            // Kiểm tra xem tên người dùng đã tồn tại chưa
            if (_context.Users.Any(u => u.UserName == user.UserName))
            {
                return false; 
            }

            // Kiểm tra xem email đã tồn tại chưa
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return false; 
            }

            // Kiểm tra độ mạnh của mật khẩu
            if (!IsValidPassword(user.Password))
            {
               
                return false;
            }

            // Mã hóa mật khẩu
            user.Password = PasswordHasher.HashPassword(user.Password);

            // Thêm người dùng mới
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Gán vai trò cho người dùng mới
            var userRole = new UserRole
            {
                UserId = user.UserID,
                RoleId = _context.Roles.First(r => r.NameRole == role).RoleID
            };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

            return true;
        }

      


        // Đăng xuất , xóa đi phần Session Cookie
        public async Task Logout()
        {
            if (_httpContextAccessor != null)
            {
                var context = _httpContextAccessor.HttpContext;
                if (context != null)
                {
                    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }
        }
    }
}
