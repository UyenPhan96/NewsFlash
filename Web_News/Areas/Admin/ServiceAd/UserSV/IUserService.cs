using Web_News.Areas.Admin.ViewModels;
using Web_News.Models;

namespace Web_News.Areas.Admin.ServiceAd.UserSV
{
    public interface IUserService
    {
        bool AccountStatus(int userId);
        bool DeleteUser(int userId);
        List<UserViewModels> GetActiveUsers(int RoleId);
        List<Role> GetAllRoles();
        List<User> GetAllUsersWithRole();
        List<UserViewModels> GetDeletedUsers(int RoleId);
        List<UserViewModels> GetLockedUsers(int RoleId);
        User GetUserById(int userId);
        bool UpdateUserInfo(UserViewModels model);
        
    }
}
