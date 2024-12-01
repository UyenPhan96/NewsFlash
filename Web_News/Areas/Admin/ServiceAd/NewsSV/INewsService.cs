using Web_News.Areas.Admin.ViewModels;
using Web_News.Models;

namespace Web_News.Areas.Admin.ServiceAd.NewsSV
{
    public interface INewsService
    {
        Task<News> CreateNewsAsync(News news, IEnumerable<int> categoryIds);
        Task<News> UpdateNewsAsync(News news, IEnumerable<int> categoryIds);
        Task DeleteNewsAsync(int newsId);
        Task<bool> DeleteAllNewsAsync(int newsId);
        Task<NewsViewModel> GetNewsByIdAsync(int newsId);
        Task<IEnumerable<NewsViewModel>> GetAllNewsByApprovalStatusAsync(ApprovalStatus? statusFilter);
        Task<List<News>> GetTop4News();
        Task<List<Advertisement>> GetActiveAdvertisementsAsync(BannerPosition position , int maxResults);
        Task<IEnumerable<NewsViewModel>> GetNewsByCategoryAsync(int categoryId);
        Task<List<News>> GetNewsByCategoryList(int categoryId, int pageNumber, int pageSize);
        Task<int> GetNewsCountByCategory(int categoryId);
        Task<List<NewsViewModel>> GetAllPendingNewsAsync();
        Task<bool> ApproveNewsAsync(int newsId);
        Task<bool> RejectNewsAsync(int newsId, string rejectionReason);
        Task<PageResult> GetNewsByApprovalStatusAsync(ApprovalStatus? status, int userId, int pageNumber, int pageSize, string titleSearchTerm = null, DateTime? publishDate = null, string authorSearchTerm = null, int? categoryId = null);
        Task<bool> UpdateNewsStatusAsync(int newsId, ApprovalStatus approvalStatus, bool status);
        Task<PageResult> GetPagedNewsByApprovalStatusAsync(ApprovalStatus? status, int pageNumber, int pageSize, string titleSearchTerm = null,
            DateTime? publishDate = null, string authorSearchTerm = null, int? categoryId = null);
        Task<List<News>> SearchNewsAsync(int? categoryId, string searchQuery, DateTime? startDate, DateTime? endDate);
        Task<List<News>> GetNewsByCategory(int categoryId, int count);
        Task<List<News>> GetTopNews360(int count);
    }
}
