using Web_News.Models;
using Web_News.ViewModels;

namespace Web_News.Services.ContactSV
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateContact(ContactViewModel model)
        {
            var advertisement = new Advertisement
            {
                ContactName = model.ContactName,
                Content = model.Content,
                Image = model.MediaFilePath,  // Đây là tên file ảnh đã được lưu
                Link = model.Link,
                Status = Status.Hidden,
                ApprovalStatus = ApprovalStatus.Pending,
            };

            _context.Advertisements.Add(advertisement);
            _context.SaveChanges();
        }

    }
}
