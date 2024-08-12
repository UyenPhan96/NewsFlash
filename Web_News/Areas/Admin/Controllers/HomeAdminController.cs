using Microsoft.AspNetCore.Mvc;

namespace Web_News.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeAdminController : Controller
    {
        public IActionResult HomePage()
        {
            //yyy
            return View();
        }
    }
}
