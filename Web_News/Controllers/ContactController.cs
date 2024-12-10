using Microsoft.AspNetCore.Mvc;
using System.IO;
using Web_News.Services.ContactSV;
using Web_News.ViewModels;

namespace Web_News.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContactController(IContactService contactService, IWebHostEnvironment webHostEnvironment)
        {
            _contactService = contactService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.MediaFile != null && model.MediaFile.Length > 0)
                {
                    var fileName = Path.GetFileName(model.MediaFile.FileName);
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesAdvertisement");
                    var filePath = Path.Combine(uploadPath, fileName);

                    // Lưu file ảnh hoặc video
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.MediaFile.CopyToAsync(stream);
                    }

                    model.MediaFilePath = fileName; // Lưu tên file vào model
                }

                await _contactService.CreateContact(model);
                TempData["success"] = "Liên hệ đã được gửi thành công!";
                return RedirectToAction("Create");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecentNotifications()
        {
            var notifications = await _contactService.GetRecentNotifications();
            return Json(notifications); // Trả về danh sách thông báo dưới dạng JSON
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int advertisementId)
        {
            await _contactService.MarkAsRead(advertisementId);
            return Ok();
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
