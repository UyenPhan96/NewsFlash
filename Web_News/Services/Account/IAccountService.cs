using Web_News.Models;

namespace Web_News.Services.Account
{
    public interface IAccountService
    {
        Task<(User? user, List<string> roles)> LoginAsync(string username, string password, bool rememberMe);
        Task<bool> RegisterAsync(User user, string role);
        Task Logout();
        Task<bool> UserNameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);

        // Thêm các phương thức cho tính năng quên mật khẩu
        Task<bool> SendPasswordResetCodeAsync(string email);
        Task<bool> ResetPasswordAsync( string resetCode, string newPassword);
        Task<User> FacebookLoginAsync(string facebookId, string name, string email);
        Task<User> GoogleLoginAsync(string googleId, string name, string email);


    }
}
