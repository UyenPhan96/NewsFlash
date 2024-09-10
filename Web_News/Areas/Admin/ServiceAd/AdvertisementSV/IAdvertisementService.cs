using Web_News.Models;

namespace Web_News.Areas.Admin.ServiceAd.AdvertisementSV
{
    public interface IAdvertisementService
    {
        Task ApproveAdvertisementAsync(int advertisementId);
        Task<Advertisement?> GetAdvertisementByIdAsync(int id);
        Task<List<Advertisement>> GetAdvertisementsAsync();
        Task RejectAdvertisementAsync(int advertisementId);
        Task SaveAdvertisementImageAsync(int advertisementId, IFormFile mediaFile);
       
    }
}
