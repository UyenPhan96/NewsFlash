﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create(ContactViewModel model)
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
                        model.MediaFile.CopyTo(stream);
                    }

                    model.MediaFilePath = fileName; // Lưu tên file vào model
                }

                _contactService.CreateContact(model);
                return RedirectToAction("Create");
            }

            return View(model);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
