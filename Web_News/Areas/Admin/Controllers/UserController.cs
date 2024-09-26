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
            List<UserViewModels> users;

            switch (status.ToLower())
            {
                case "locked":
                    users = _userService.GetLockedUsers(2);
                    ViewBag.CurrentStatus = "locked";
                    break;
                case "deleted":
                    users = _userService.GetDeletedUsers(2);
                    ViewBag.CurrentStatus = "deleted";
                    break;
                case "active":
                default:
                    users = _userService.GetActiveUsers(2);
                    ViewBag.CurrentStatus = "active";
                    break;
            }

            return View(users);
        }
        // Lấy danh sách cho 3 trạng thái admin
        public ActionResult ManageAdmins(string status = "active")
        {
            List<UserViewModels> users;

            switch (status.ToLower())
            {
                case "locked":
                    users = _userService.GetLockedUsers(0);
                    ViewBag.CurrentStatus = "locked";
                    break;
                case "deleted":
                    users = _userService.GetDeletedUsers(0);
                    ViewBag.CurrentStatus = "deleted";
                    break;
                case "active":
                default:
                    users = _userService.GetActiveUsers(0);
                    ViewBag.CurrentStatus = "active";
                    break;
            }

            return View(users);
        }

        // Thay đổi trạng thái để khóa tài khoản và mở tài khoản
        [HttpPost]
        public ActionResult AccountStatus(int userId, string status, string returnUrl)
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

            // Nếu returnUrl có giá trị, chuyển hướng về đúng trang mà người dùng đang thao tác
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            // Nếu không có returnUrl, mặc định chuyển hướng về ManageUsers
            return RedirectToAction("ManageUsers", new { status = status });
        }

        // thay đổi trạng thái để xóa tài khoản
        [HttpPost]
        public IActionResult DeleteUser(int userId, string status, string returnUrl)
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

            // Nếu returnUrl có giá trị, chuyển hướng về đúng trang mà người dùng đang thao tác
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            // Nếu không có returnUrl, mặc định chuyển hướng về ManageUsers
            return RedirectToAction("ManageUsers", new { status = status });
        }

        // Cập nhật lại thông tin cho người dùng hoặc quản trị viên
        public ActionResult EditUser(int userId, string returnUrl, bool isAdmin = false)
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

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.IsAdmin = isAdmin; // Lưu thông tin xem có phải là admin hay không
            return View(viewModel); // Trả về ViewModel cho View
        }


        [HttpPost]
        public IActionResult EditUser(UserViewModels model, bool isAdmin = false)
        {
            if (ModelState.IsValid)
            {
                // Nếu model không hợp lệ, trả về view với model hiện tại
                model.Roles = _userService.GetAllRoles();
                return View(model);
            }

            // Tiến hành cập nhật thông tin người dùng
            var result = _userService.UpdateUserInfo(model);
            if (result)
            {
                return Redirect(ViewBag.ReturnUrl ?? "ManageUsers");
            }

            // Nếu không cập nhật được, thêm thông báo lỗi vào ModelState
            ModelState.AddModelError("", "Không thể cập nhật thông tin người dùng.");

            // Nạp lại danh sách vai trò nếu có lỗi
            model.Roles = _userService.GetAllRoles();
            return View(model);
        }




    }
}
