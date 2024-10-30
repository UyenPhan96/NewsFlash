using Web_News.ViewModels;

namespace Web_News.Services.ContactSV
{
    public interface IContactService
    {
      
        Task CreateContact(ContactViewModel model);
    }
}
