using Web_News.Areas.Admin.ViewModels;
using Web_News.Models;

namespace Web_News.Areas.Admin.ServiceAd.UserSV
{
    public interface IUserService
    {
        bool AccountStatus(int userId);
        bool DeleteUser(int userId);
        List<User> GetActiveUsers();
        List<Role> GetAllRoles();
        List<User> GetAllUsersWithRole();
        List<User> GetDeletedUsers();
        List<User> GetLockedUsers();
        User GetUserById(int userId);
        bool UpdateUserInfo(UserViewModels model);
        
    }
}
