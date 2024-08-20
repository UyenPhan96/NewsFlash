using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Web_News.Models;
using Web_News.Services.PasswordH;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Web_News.Services.EmailService;
using System.Net.Mail;

namespace Web_News.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        public AccountService(AppDbContext context, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
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
        public async Task<(User? user, List<string> roles)> LoginAsync(string usernameOrEmail, string password,bool rememberMe)
        {
            if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
            {
                return (null, new List<string>());
            }

            var user = _context.Users.SingleOrDefault(u => (u.UserName == usernameOrEmail || u.Email == usernameOrEmail));

            if (user != null)
            {
                    // Kiểm tra mật khẩu
                if (PasswordHasher.VerifyPassword(user.Password, password))
                {
               
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
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = rememberMe,  
                        ExpiresUtc = rememberMe ? DateTime.UtcNow.AddDays(14) : DateTime.UtcNow.AddMinutes(20)
                    };
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


        public async Task<bool> SendPasswordResetCodeAsync(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return false; 
            }

            var resetCode = GenerateResetCode();
            user.PasswordResetCode = resetCode;
            user.ResetCodeExpiration = DateTime.UtcNow.AddMinutes(1); // 1 phút
            await _context.SaveChangesAsync();

            var subject = "Yêu cầu đặt lại mật khẩu";
            var message = $@"
            <p>Xin chào {user.Name},</p>
            <p>Bạn đã yêu cầu đặt lại mật khẩu cho tài khoản của mình.</p>
            <p>Mã xác nhận của bạn là:</p>
            <div style='border: 2px solid #000; padding: 10px; text-align: center; display: inline-block; font-size: 24px; font-weight: bold;'>
                {resetCode}
            </div>
            <p>Mã sẽ hết hạn sau 1 giờ.</p>
            <p>Nếu bạn không yêu cầu điều này, hãy bỏ qua email này và mật khẩu của bạn sẽ không thay đổi.</p>
            <p>Trân trọng,<br/>Đội ngũ hỗ trợ</p>";

            try
            {
                await _emailService.SendEmailAsync(email, subject, message);
                return true; // Email được gửi thành công
            }
            catch (SmtpFailedRecipientException)
            {
                return false; // Trường hợp email không hợp lệ
            }
            catch (Exception)
            {
                return false; // Các lỗi khác 
            }
        }


        private string GenerateResetCode()
        {
            
            return Guid.NewGuid().ToString("N").Substring(0, 6);
        }

        // Phương thức xác thực mã và đổi mật khẩu
        public async Task<bool> ResetPasswordAsync(string resetCode, string newPassword)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.PasswordResetCode == resetCode);
            if (user == null || user.ResetCodeExpiration < DateTime.UtcNow)
            {
                return false;
            }

            if (!IsValidPassword(newPassword))
            {
                return false;
            }

            user.Password = PasswordHasher.HashPassword(newPassword);
            user.PasswordResetCode = null;
            user.ResetCodeExpiration = null;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User> FacebookLoginAsync(string facebookId, string name, string email)
        { 
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                user = new User
                {
                    Name = name,
                    Email = email,
                    UserName = email,  
                    Password = "",  
                    RegistrationDate = DateTime.Now,
                    Optional = facebookId
                    
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                var userRole = new UserRole { UserId = user.UserID, RoleId = 2 }; 
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Cập nhật FacebookId nếu chưa có
                if (string.IsNullOrEmpty(user.Optional))
                {
                    user.Optional = facebookId;
                }
                // Nếu người dùng đã tồn tại, cập nhật thông tin
                user.Name = name;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }
    }
}
