using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_News.Areas.Admin.ViewModels;

namespace Web_News.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ImageController : Controller
    {
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        // GET: Image
        public IActionResult Index()
        {
            var files = Directory.GetFiles(_imagePath);
            var images = files.Select(f => Path.GetFileName(f)).ToList();
            return View(images);
        }

        // GET: Image/GetImages
        [HttpGet]
        public IActionResult GetImages()
        {
            var files = Directory.GetFiles(_imagePath);
            var images = files.Select(f => Path.GetFileName(f)).ToList();
            return Json(images);
        }
        // POST: Image/UploadImage
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(_imagePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public IActionResult DeleteImage([FromBody] ImageDeleteModel model)
        {
            if (string.IsNullOrEmpty(model?.ImageName))
            {
                return Json(new { success = false, message = "Invalid image name." });
            }

            var filePath = Path.Combine(_imagePath, model.ImageName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "File not found." });
        }

   
    }
}
