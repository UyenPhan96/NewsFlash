using Web_News.Models;

namespace Web_News.Areas.Admin.ViewModels
{
    public class UserViewModels
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string UserName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Role { get; set; }
        public bool AccountStatus { get; set; }
        public List<int> SelectedRoles { get; set; } // Vai trò được chọn
        public List<Role> Roles { get; set; } // Danh sách các vai trò có sẵn
    }
}
