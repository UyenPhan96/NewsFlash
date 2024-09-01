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


        public async Task<IEnumerable<NewsViewModel>> GetAllNewsAsync()
        {
            return await _context.News
                .Select(n => new NewsViewModel
                {
                    NewsId = n.NewsId,
                    Title = n.Title,
                    Image = n.Image,
                    PublishDate = n.PublishDate,
                    Status = n.Status,
                    CreatedByUserName = _context.Users.FirstOrDefault(u => u.UserID == n.CreatedByUserId).Name
                })
                .ToListAsync();
        }

    }
}
