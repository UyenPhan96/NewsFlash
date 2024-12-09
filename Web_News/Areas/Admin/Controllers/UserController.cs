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
      
        public ActionResult ManageAdmins(int roleId ,string status = "active")
        {
           
            List<UserViewModels> users;

            switch (status.ToLower())
            {
                case "locked":
                    users = _userService.GetLockedUsers(roleId);
                    ViewBag.CurrentStatus = "locked";
                    break;
                case "deleted":
                    users = _userService.GetDeletedUsers(roleId);
                    ViewBag.CurrentStatus = "deleted";
                    break;
                case "active":
                default:
                    users = _userService.GetActiveUsers(roleId);
                    ViewBag.CurrentStatus = "active";
                    break;
            }
            ViewBag.RoleId = roleId;
            return View(users);
        }

        // Thay đổi trạng thái để khóa tài khoản và mở tài khoản
        [HttpPost]
        public ActionResult AccountStatus(int userId, string status, string returnUrl)
        {
            bool result = _userService.AccountStatus(userId);

            if (result)
            {
                TempData["success"] = "Tài khoản đã được khóa.";
            }
            else
            {
                TempData["Error"] = "Không tìm thấy người dùng.";
            }
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("ManageUsers", new { status = status });
        }

        // thay đổi trạng thái để xóa tài khoản
        [HttpPost]
        public IActionResult DeleteUser(int userId, string status, string returnUrl)
        {
            bool result = _userService.DeleteUser(userId);

            if (result)
            {
                TempData["success"] = "Tài khoản đã được xóa.";
            }
            else
            {
                TempData["error"] = "Không tìm thấy người dùng.";
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
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
                Roles = _userService.GetAllRoles() 
            };

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.IsAdmin = isAdmin; 
            return View(viewModel); 
        }


        [HttpPost]
        public IActionResult EditUser(UserViewModels model, string returnUrl, bool isAdmin = false)
        {
            if (ModelState.IsValid)
            {
                model.Roles = _userService.GetAllRoles();
                return View(model);
            }
            var result = _userService.UpdateUserInfo(model);
            if (result)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("ManageUsers");
            }
            ModelState.AddModelError("", "Không thể cập nhật thông tin người dùng.");
            model.Roles = _userService.GetAllRoles();
            return View(model);
        }

        public ActionResult ManageRoles()
        {
            var roles = _userService.GetRolesWithUserCounts();
            return View(roles); // Hiển thị danh sách role
        }




    }
}
