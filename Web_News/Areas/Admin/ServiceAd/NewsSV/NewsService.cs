using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Web_News.Areas.Admin.ViewModels;
using Web_News.Models;

namespace Web_News.Areas.Admin.ServiceAd.NewsSV
{
    public class NewsService : INewsService
    {
        private readonly AppDbContext _context;

        public NewsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<News> CreateNewsAsync(News news, IEnumerable<int> categoryIds)
        {
            news.PublishDate = DateTime.Now;

            // Thêm bản ghi News vào trước để có thể lấy được NewsId
            _context.News.Add(news);
            await _context.SaveChangesAsync();  // Lưu để có được NewsId

            // Thêm các thể loại liên quan
            foreach (var categoryId in categoryIds)
            {
                _context.NewsCategories.Add(new NewsCategory
                {
                    NewsId = news.NewsId,  // Sử dụng NewsId từ bản ghi News đã lưu
                    CategoryId = categoryId
                });
            }

            // Lưu tất cả thay đổi
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<News> UpdateNewsAsync(News news, IEnumerable<int> categoryIds)
        {
            var existingNews = await _context.News.Include(n => n.NewsCategories)
                                                 .FirstOrDefaultAsync(n => n.NewsId == news.NewsId);
            if (existingNews == null) throw new ArgumentException("News not found");

            existingNews.Title = news.Title;
            existingNews.Content = news.Content;
            existingNews.Image = news.Image;
            existingNews.Status = news.Status;

            // Remove existing categories
            _context.NewsCategories.RemoveRange(existingNews.NewsCategories);

            // Add updated categories
            foreach (var categoryId in categoryIds)
            {
                _context.NewsCategories.Add(new NewsCategory
                {
                    NewsId = existingNews.NewsId,
                    CategoryId = categoryId
                });
            }

            await _context.SaveChangesAsync();
            return existingNews;
        }

        public async Task DeleteNewsAsync(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            if (news == null) throw new ArgumentException("News not found");

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NewsViewModel>> GetAllNewsAsync()
        {

            return await _context.News
         .Include(n => n.NewsCategories) // Bao gồm liên kết tới bảng NewsCategories
         .ThenInclude(nc => nc.Category) // Bao gồm thông tin danh mục từ bảng Category
         .Select(n => new NewsViewModel
         {
             NewsId = n.NewsId,
             Title = n.Title,
             Image = n.Image,
             PublishDate = n.PublishDate,
             Status = n.Status,
             CreatedByUserName = _context.Users.FirstOrDefault(u => u.UserID == n.CreatedByUserId).Name,
             ListCategories = n.NewsCategories.Select(nc => nc.Category).ToList() // Lấy danh sách các danh mục
         })
         .ToListAsync();
        }

        public async Task<NewsViewModel> GetNewsByIdAsync(int newsId)
        {
            var news = await _context.News.Include(n => n.NewsCategories)
                                          .ThenInclude(nc => nc.Category)
                                          .FirstOrDefaultAsync(n => n.NewsId == newsId);

            if (news == null)
            {
                return null;
            }

            return new NewsViewModel
            {
                NewsId = news.NewsId,
                Title = news.Title,
                Content = news.Content,
                Image = news.Image,
                PublishDate = news.PublishDate,
                CreatedByUserName = _context.Users.FirstOrDefault(u => u.UserID == news.CreatedByUserId)?.Name,
                Status = news.Status,
                Categories = news.NewsCategories.Select(nc => nc.Category).ToList() // Thay đổi ở đây
            };
        }
        public async Task<List<News>> GetTop4News()
        {
            return await _context.News
                .Where(n => n.Status) 
                .OrderByDescending(n => n.PublishDate)
                .Take(4)
                .ToListAsync();
        }

        public async Task<IEnumerable<NewsViewModel>> GetNewsByCategoryAsync(int categoryId)
        {
            return await _context.News
                .Include(n => n.NewsCategories)
                .ThenInclude(nc => nc.Category)
                .Where(n => n.NewsCategories.Any(nc => nc.CategoryId == categoryId)) // Lọc theo CategoryId
                .Select(n => new NewsViewModel
                {
                    NewsId = n.NewsId,
                    Title = n.Title,
                    Image = n.Image,
                    PublishDate = n.PublishDate,
                    Status = n.Status,
                    CreatedByUserName = _context.Users.FirstOrDefault(u => u.UserID == n.CreatedByUserId).Name,
                    ListCategories = n.NewsCategories.Select(nc => nc.Category).ToList()
                })
                .ToListAsync();
        }
        public async Task<List<News>> GetNewsByCategoryList(int categoryId, int pageNumber, int pageSize)
        {
            // Lọc các bài viết thuộc danh mục
            var query = _context.News
                .Where(n => n.NewsCategories.Any(nc => nc.CategoryId == categoryId) && n.Status);

            // Phân trang
            return await query
                .OrderByDescending(n => n.PublishDate) // Sắp xếp theo ngày đăng
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các mục trước trang hiện tại
                .Take(pageSize) // Lấy số mục theo kích thước trang
                .ToListAsync();
        }

        public async Task<int> GetNewsCountByCategory(int categoryId)
        {
            return await _context.News
                .CountAsync(n => n.NewsCategories.Any(nc => nc.CategoryId == categoryId) && n.Status);
        }






        public async Task<List<Advertisement>> GetActiveAdvertisementsAsync(BannerPosition position,int maxResults)
        {
            var currentDate = DateTime.Now;

            // Lấy các quảng cáo dựa trên vị trí quảng cáo, trạng thái được hiển thị và còn hiệu lực
            var advertisements = await _context.Advertisements
                .Where(ad => ad.Status == Status.Displayed &&
                             ad.ApprovalStatus == ApprovalStatus.Approved &&
                             ad.BannerPosition == position && 
                             ad.EndDate > currentDate) 
                .Take (maxResults)
                .ToListAsync();

            
            // Cập nhật trạng thái của các quảng cáo đã hết hạn
            var expiredAdvertisements = await _context.Advertisements
                .Where(ad => ad.Status == Status.Displayed && ad.EndDate <= currentDate)
                .ToListAsync();
            // Lấy ra danh sách hết hạn và cập nhật trạng thái hiển thị 
            foreach (var ad in expiredAdvertisements)
            {
                ad.Status = Status.Expired;
            }

          
            if (expiredAdvertisements.Any())
            {
                await _context.SaveChangesAsync();
            }

            return advertisements;
        }


        // Lấy danh sách bài viết theo thể loại và quảng cáo 
        public async Task<ListViewModel> GetNewsByCategoryWithAdsAsync(int categoryId)
        {
            // Lấy danh sách bài viết theo thể loại (CategoryId)
            var newsList = await _context.News
                .Include(n => n.NewsCategories)
                .ThenInclude(nc => nc.Category)
                .Where(n => n.NewsCategories.Any(nc => nc.CategoryId == categoryId) && n.Status) // Lọc theo CategoryId và trạng thái hiển thị
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();

            // Lấy quảng cáo cho Sidebar (5 quảng cáo)
            var sidebarAds = await GetActiveAdvertisementsAsync(BannerPosition.Sidebar, 5);

            // Lấy quảng cáo cho Header (1 quảng cáo)
            var headerAds = await GetActiveAdvertisementsAsync(BannerPosition.Header, 1);

            // Lấy quảng cáo cho Footer (1 quảng cáo)
            var footerAds = await GetActiveAdvertisementsAsync(BannerPosition.Footer, 1);

            // Tạo ListViewModel chứa bài viết và quảng cáo
            var viewModel = new ListViewModel
            {
                LatestNews = newsList,
                SidebarAdvertisements = sidebarAds,
                HeaderAdvertisements = headerAds,
                FooterAdvertisements = footerAds
            };

            return viewModel;
        }


    }
}
