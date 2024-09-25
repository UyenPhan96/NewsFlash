using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_News.Areas.Admin.ServiceAd.UserSV;
using Web_News.Areas.Admin.ViewModels;
using Web_News.Models;

namespace Web_News.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Lấy danh sách cho 3 trạng thái 
        public ActionResult ManageUsers(string status = "active")
        {
            List<User> users;

            switch (status.ToLower())
            {
                case "locked":
                    users = _userService.GetLockedUsers();
                    ViewBag.CurrentStatus = "locked";
                    break;
                case "deleted":
                    users = _userService.GetDeletedUsers();
                    ViewBag.CurrentStatus = "deleted";
                    break;
                case "active":
                default:
                    users = _userService.GetActiveUsers();
                    ViewBag.CurrentStatus = "active";
                    break;
            }

            return View(users);
        }

  
        // Thay đổi trạng thái để khóa tài khoản và mở tài khoản
        [HttpPost]
        public ActionResult AccountStatus(int userId)
        {
            bool result = _userService.AccountStatus(userId);

            if (result)
            {
                TempData["Message"] = "Đã thay đổi trạng thái tài khoản.";
            }
            else
            {
                TempData["Error"] = "Không tìm thấy người dùng.";
            }

            return RedirectToAction("ManageUsers");
        }

        // thay đổi trạng thái để xóa tài khoản
        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            bool result = _userService.DeleteUser(userId);

            if (result)
            {
                TempData["Message"] = "Tài khoản đã được xóa.";
            }
            else
            {
                TempData["Error"] = "Không tìm thấy người dùng.";
            }

            return RedirectToAction("ManageUsers");
        }

        // cập nhật lại thông tin cho người dùng
        public ActionResult EditUser(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserViewModels
            {
                UserId = user.UserID,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                UserName = user.UserName,
                SelectedRoles = user.UserRoles.Select(ur => ur.RoleId).ToList(),
                Roles = _userService.GetAllRoles() // Lấy danh sách vai trò
            };

            return View(viewModel); // Trả về ViewModel cho View
        }

        [HttpPost]
        public IActionResult EditUser(UserViewModels model)
        {
            if (!ModelState.IsValid)
            {
                var result = _userService.UpdateUserInfo(model);
                if (result)
                {
                    return RedirectToAction("ManageUsers");
                }

                ModelState.AddModelError("", "Không thể cập nhật thông tin người dùng.");
            }

            // Nếu có lỗi, nạp lại danh sách vai trò
            model.Roles = _userService.GetAllRoles();
            return View(model);
        }


    }
}
