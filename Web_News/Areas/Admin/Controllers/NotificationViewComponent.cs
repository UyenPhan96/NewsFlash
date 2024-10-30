using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_News.Models;

namespace Web_News.Areas.Admin.Controllers
{
    public class NotificationViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public NotificationViewComponent(AppDbContext context)
        {
            _context = context;
        }

   
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var newAds = _context.Advertisements
                        .Where(ad => ad.ApprovalStatus == ApprovalStatus.Pending)
                        .OrderByDescending(ad => ad.CreatedDate)
                        .Take(5)
                        .ToList();

            return View(newAds);
        }
    }
}
