using Web_News.Models;

namespace Web_News.Services.Account
{
    public interface IAccountService
    {
        Task<(User? user, List<string> roles)> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(User user, string role);
        Task Logout();
        Task<bool> UserNameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
    }
}
