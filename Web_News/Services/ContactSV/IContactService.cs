using Web_News.Areas.Admin.ViewModels;
using Web_News.ViewModels;

namespace Web_News.Services.ContactSV
{
    public interface IContactService
    {
      
        Task CreateContact(ContactViewModel model);
        Task<List<NotificationViewModel>> GetRecentNotifications();
        Task MarkAsRead(int advertisementId);
    }
}
