using Microsoft.AspNetCore.SignalR;
using Web_News.Areas.Admin.Hubs;
using Web_News.Models;
using Web_News.ViewModels;

namespace Web_News.Services.ContactSV
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ContactService(AppDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task CreateContact(ContactViewModel model)
        {
            var advertisement = new Advertisement
            {
                ContactName = model.ContactName,
                Content = model.Content,
                Image = model.MediaFilePath,
                Link = model.Link,
                Status = Status.Hidden,
                ApprovalStatus = ApprovalStatus.Pending,
            };

            _context.Advertisements.Add(advertisement);
            await _context.SaveChangesAsync();

            // Gửi thông báo đến tất cả các client với thông tin chi tiết
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", new
            {
                advertisementId = advertisement.AdvertisementId,
                contactName = advertisement.ContactName,
                content = advertisement.Content
            });
        }
    }
}
