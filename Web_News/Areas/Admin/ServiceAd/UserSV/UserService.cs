using Microsoft.EntityFrameworkCore;
using Web_News.Areas.Admin.ViewModels;
using Web_News.Models;

namespace Web_News.Areas.Admin.ServiceAd.UserSV
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách người dùng có Role là "User"
        public List<User> GetAllUsersWithRole()
        {
            // Giả sử RoleId = 2 là của "User"
            int userRoleId = 2;

            var usersWithRoleUser = (from ur in _context.UserRoles
                                     where ur.RoleId == userRoleId
                                     join u in _context.Users on ur.UserId equals u.UserID
                                     where u.IsDeleted == false && u.AccountStatus == false
                                     select u).ToList();

            return usersWithRoleUser;
        }

        // Lấy danh sách người dùng hoạt động (không bị xóa và không bị khóa)
        public List<User> GetActiveUsers()
        {
            int userRoleId = 2; // Giả sử RoleId = 2 là của "User"

            return (from ur in _context.UserRoles
                    where ur.RoleId == userRoleId
                    join u in _context.Users on ur.UserId equals u.UserID
                    where u.IsDeleted == false && u.AccountStatus == false
                    select u).ToList();
        }

        // Lấy danh sách người dùng bị khóa
        public List<User> GetLockedUsers()
        {
            int userRoleId = 2; // Giả sử RoleId = 2 là của "User"

            return (from ur in _context.UserRoles
                    where ur.RoleId == userRoleId
                    join u in _context.Users on ur.UserId equals u.UserID
                    where u.AccountStatus == true && u.IsDeleted == false
                    select u).ToList();
        }

        // Lấy danh sách người dùng đã bị xóa
        public List<User> GetDeletedUsers()
        {
            int userRoleId = 2; // Giả sử RoleId = 2 là của "User"

            return (from ur in _context.UserRoles
                    where ur.RoleId == userRoleId
                    join u in _context.Users on ur.UserId equals u.UserID
                    where u.IsDeleted == true 
                    select u).ToList();
        }

        public bool AccountStatus(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);

            if (user != null)
            {
                // Đổi trạng thái tài khoản
                user.AccountStatus = !user.AccountStatus;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);

            if (user != null)
            {
                
                user.IsDeleted = true; // Đánh dấu tài khoản đã bị xóa
                _context.SaveChanges();
                return true;
            }

            return false;
        }


        // Lấy người dùng theo ID
        public User GetUserById(int userId)
        {
            return _context.Users.Include(u => u.UserRoles)
                                 .ThenInclude(ur => ur.Role)
                                 .FirstOrDefault(u => u.UserID == userId);
        }

 
      
        public bool UpdateUserInfo(UserViewModels model)
        {
            var user = _context.Users.Include(u => u.UserRoles)
                                     .FirstOrDefault(u => u.UserID == model.UserId);

            if (user == null)
            {
                return false;
            }

            // Cập nhật thông tin người dùng
            user.Name = model.Name;
            user.Address = model.Address;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.UserName = model.UserName;

            // Cập nhật vai trò
            user.UserRoles.Clear();
            if (model.SelectedRoles != null && model.SelectedRoles.Count > 0)
            {
                foreach (var roleId in model.SelectedRoles)
                {
                    user.UserRoles.Add(new UserRole
                    {
                        UserId = user.UserID,
                        RoleId = roleId
                    });
                }
            }

            _context.SaveChanges();
            return true;
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
