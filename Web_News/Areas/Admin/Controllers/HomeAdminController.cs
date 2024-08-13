using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Web_News.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeAdminController : Controller
    {
        
        public IActionResult HomePage()
        {
            // jjj
            return View();
        }
    }
}
