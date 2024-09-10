using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_News.Areas.Admin.ServiceAd.AdvertisementSV;
using Web_News.Models;

namespace Web_News.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        // Hiển thị danh sách quảng cáo
        public async Task<IActionResult> Index()
        {
            var advertisements = await _advertisementService.GetAdvertisementsAsync();
            return View(advertisements);
        }

        // Chi tiết quảng cáo
        public async Task<IActionResult> Approve(int id)
        {
            var advertisement = await _advertisementService.GetAdvertisementByIdAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return View(advertisement);
        }

        // Phương thức POST để duyệt quảng cáo và cập nhật thông tin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id, Advertisement model, IFormFile MediaFile)
        {
            try
            {
                var advertisement = await _advertisementService.GetAdvertisementByIdAsync(id);

                if (advertisement == null)
                {
                    return NotFound();
                }

                advertisement.Link = model.Link;
                advertisement.StartDate = model.StartDate;
                advertisement.EndDate = model.EndDate;
                advertisement.BannerPosition = model.BannerPosition;

                 // Nếu có ảnh mới được chọn, lưu và cập nhật hình ảnh
                if (MediaFile != null && MediaFile.Length > 0)
                {
                    await _advertisementService.SaveAdvertisementImageAsync(id, MediaFile);
                }
              
                // Duyệt quảng cáo
                await _advertisementService.ApproveAdvertisementAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing the advertisement.");
                return View("Error");
            }
        }

        // Từ chối quảng cáo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                var advertisement = await _advertisementService.GetAdvertisementByIdAsync(id);

                if (advertisement == null)
                {
                    return NotFound();
                }

                // Từ chối quảng cáo và cập nhật trạng thái
                await _advertisementService.RejectAdvertisementAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while rejecting the advertisement.");
                return View("Error");
            }
        }


    }
}
