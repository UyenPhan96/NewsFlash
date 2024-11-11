using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Web_News.Areas.Admin.Hubs;
using Web_News.Areas.Admin.ViewModels;
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
            // Gửi thông báo tới tất cả client
            await _hubContext.Clients.All.SendAsync("ReceiveNotification",
                advertisement.AdvertisementId.ToString(),
                advertisement.ContactName,
                advertisement.Content,
                advertisement.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"));

        }
        // Lấy 10 thông báo gần đây nhất
        public async Task<List<NotificationViewModel>> GetRecentNotifications()
        {
            return await _context.Advertisements
                .OrderByDescending(a => a.CreatedDate)
                .Take(10)
                .Select(a => new NotificationViewModel
                {
                    AdvertisementId = a.AdvertisementId,
                    ContactName = a.ContactName,
                    Content = a.Content,
                    CreatedDate = a.CreatedDate,
                    IsRead = a.IsRead
                })
                .ToListAsync();
        }

        // Đánh dấu một thông báo là đã đọc
        public async Task MarkAsRead(int advertisementId)
        {
            var advertisement = await _context.Advertisements.FindAsync(advertisementId);
            if (advertisement != null)
            {
                advertisement.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
