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

        /// <summary>
        /// Tạo bài viết mới
        /// </summary>
        /// <param name="news"></param>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        public async Task<News> CreateNewsAsync(News news, IEnumerable<int> categoryIds)
        {
            news.PublishDate = DateTime.Now;

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
            await _context.SaveChangesAsync();
            return news;
        }

        /// <summary>
        /// Update bài viết mới
        /// </summary>
        /// <param name="news"></param>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<News> UpdateNewsAsync(News news, IEnumerable<int> categoryIds)
        {
            var existingNews = await _context.News.Include(n => n.NewsCategories)
                                                 .FirstOrDefaultAsync(n => n.NewsId == news.NewsId);
            if (existingNews == null) throw new ArgumentException("News not found");

            existingNews.Title = news.Title;
            existingNews.Content = news.Content;
            existingNews.Image = news.Image;
            existingNews.Status = news.Status;
            existingNews.ApprovalStatus = news.ApprovalStatus;

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

        /// <summary>
        /// Sửa bài viết 
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task DeleteNewsAsync(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            if (news == null) throw new ArgumentException("News not found");

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAllNewsAsync(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            if (news == null)
            {
                return false; // Bài viết không tồn tại
            }

            _context.News.Remove(news);
            await _context.SaveChangesAsync();

            return true; // Xóa thành công
        }


        public async Task<IEnumerable<NewsViewModel>> GetAllNewsByApprovalStatusAsync(ApprovalStatus? statusFilter)
        {
            var query = _context.News.Include(n => n.NewsCategories)
                                     .ThenInclude(nc => nc.Category)
                                     .Include(n => n.CreatedByUser) // Nạp thông tin người dùng
                                     .Select(n => new NewsViewModel
                                     {
                                         NewsId = n.NewsId,
                                         Title = n.Title,
                                         Image = n.Image,
                                         PublishDate = n.PublishDate,
                                         Status = n.Status,
                                         ApprovalStatus = n.ApprovalStatus,
                                         CreatedByUserName = n.CreatedByUser.Name, // Lấy tên người dùng
                                         ListCategories = n.NewsCategories.Select(nc => nc.Category).ToList()
                                     });

            if (statusFilter.HasValue)
            {
                query = query.Where(n => n.ApprovalStatus == statusFilter.Value);
            }

            return await query.ToListAsync();
        }

   
        /// <summary>
        /// Danh sách chính || bao gồm lọc theo trạng thái , tìm kiếm theo tiêu đề , ngày viết ,người viết, phân trang .
        /// </summary>
        /// <param name="status"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="titleSearchTerm"></param>
        /// <param name="publishDate"></param>
        /// <param name="authorSearchTerm"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<PageResult> GetPagedNewsByApprovalStatusAsync(ApprovalStatus? status, int pageNumber, int pageSize, string titleSearchTerm = null,
            DateTime? publishDate = null, string authorSearchTerm = null, int? categoryId = null)
        {
            var query = _context.News.Include(n => n.NewsCategories)
                                     .ThenInclude(nc => nc.Category)
                                     .Include(n => n.CreatedByUser)
                                     .AsQueryable();

            // Lọc theo trạng thái nếu có
            if (status.HasValue)
            {
                query = query.Where(n => n.ApprovalStatus == status.Value);
            }
            // Tìm kiếm theo tiêu đề
            if (!string.IsNullOrEmpty(titleSearchTerm))
            {
                query = query.Where(n => n.Title.Contains(titleSearchTerm));
            }
            // Tìm kiếm theo ngày viết
            if (publishDate.HasValue)
            {
                query = query.Where(n => n.PublishDate.Date == publishDate.Value.Date);
            }
            // Tìm kiếm theo người viết
            if (!string.IsNullOrEmpty(authorSearchTerm))
            {
                query = query.Where(n => n.CreatedByUser.Name.Contains(authorSearchTerm));
            }
            // Tìm kiếm theo thể loại
            if (categoryId.HasValue)
            {
                query = query.Where(n => n.NewsCategories.Any(nc => nc.CategoryId == categoryId.Value));
            }
            // Sắp xếp theo ngày phát hành, bài viết mới nhất trước
            query = query.OrderByDescending(n => n.PublishDate);

            var totalNews = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalNews / (double)pageSize);

            var news = await query.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .Select(n => new NewsViewModel
                                  {
                                      NewsId = n.NewsId,
                                      Title = n.Title,
                                      Image = n.Image,
                                      PublishDate = n.PublishDate,
                                      Status = n.Status,
                                      ApprovalStatus = n.ApprovalStatus,
                                      CreatedByUserName = n.CreatedByUser.Name,
                                      ListCategories = n.NewsCategories.Select(nc => nc.Category).ToList()
                                  })
                                  .ToListAsync();

            return new PageResult
            {
                News = news,
                TotalPages = totalPages
            };
        }


        /// <summary>
        /// cập nhạt trạng thái 
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="approvalStatus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<bool> UpdateNewsStatusAsync(int newsId, ApprovalStatus approvalStatus, bool status)
        {
            var news = await _context.News.FindAsync(newsId);
            if (news == null)
            {
                return false; // Bài viết không tồn tại
            }

            news.ApprovalStatus = approvalStatus;
            news.Status = status;

            _context.News.Update(news);
            await _context.SaveChangesAsync();

            return true; // Cập nhật thành công
        }


        /// <summary>
        /// Hiển thị bài viết theo id người tạo bài viết và trạng thái duyệt
        /// </summary>
        /// <param name="statusFilter"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<PageResult> GetNewsByApprovalStatusAsync(ApprovalStatus? status, int userId, int pageNumber, int pageSize, string titleSearchTerm = null, DateTime? publishDate = null, string authorSearchTerm = null, int? categoryId = null)
        {
            var query = _context.News.Include(n => n.NewsCategories)
                                     .ThenInclude(nc => nc.Category)
                                     .Include(n => n.CreatedByUser)
                                     .Where(n => n.CreatedByUserId == userId) // Lọc theo người viết
                                     .AsQueryable();

            // Lọc theo trạng thái nếu có
            if (status.HasValue)
            {
                query = query.Where(n => n.ApprovalStatus == status.Value);
            }
            // Tìm kiếm theo tiêu đề
            if (!string.IsNullOrEmpty(titleSearchTerm))
            {
                query = query.Where(n => n.Title.Contains(titleSearchTerm));
            }
            // Tìm kiếm theo ngày phát hành
            if (publishDate.HasValue)
            {
                query = query.Where(n => n.PublishDate.Date == publishDate.Value.Date);
            }
            // Tìm kiếm theo người viết
            if (!string.IsNullOrEmpty(authorSearchTerm))
            {
                query = query.Where(n => n.CreatedByUser.Name.Contains(authorSearchTerm));
            }
            // Tìm kiếm theo thể loại
            if (categoryId.HasValue)
            {
                query = query.Where(n => n.NewsCategories.Any(nc => nc.CategoryId == categoryId.Value));
            }
            // Sắp xếp theo ngày phát hành, bài viết mới nhất trước
            query = query.OrderByDescending(n => n.PublishDate);

            // Tính tổng số bài viết và tổng số trang
            var totalNews = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalNews / (double)pageSize);

            var news = await query.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .Select(n => new NewsViewModel
                                  {
                                      NewsId = n.NewsId,
                                      Title = n.Title,
                                      Image = n.Image,
                                      PublishDate = n.PublishDate,
                                      Status = n.Status,
                                      ApprovalStatus = n.ApprovalStatus,
                                      CreatedByUserName = n.CreatedByUser.Name,
                                      ListCategories = n.NewsCategories.Select(nc => nc.Category).ToList()
                                  })
                                  .ToListAsync();

            return new PageResult
            {
                News = news,
                TotalPages = totalPages
            };
        }



        /// <summary>
        /// Lấy bài viết theo id 
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Lấy 4 bài viết mới nhất theo ngày
        /// </summary>
        /// <returns></returns>
        public async Task<List<News>> GetTop4News()
        {
            return await _context.News
                .Where(n => n.Status) 
                .OrderByDescending(n => n.PublishDate)
                .Take(4)
                .ToListAsync();
        }

        /// <summary>
        /// Hiển thị bài viết theo thẻ loại (đang không sử dụng)
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Lấy bài viết theo thể loại và phân trang giành cho bài viết
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
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


        // Tìm kiếm bài viết tổng quát hoặc có thể thêm điều kiện tìm kiếm nâng cao
        public async Task<List<News>> SearchNewsAsync(int? categoryId, string searchQuery, DateTime? startDate, DateTime? endDate)
        {
    
            var newsQuery = _context.News
                .Include(n => n.NewsCategories)
                .ThenInclude(nc => nc.Category)
                .Where(n => n.Status); 

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                newsQuery = newsQuery.Where(n => n.NewsCategories.Any(nc => nc.CategoryId == categoryId.Value));
            }

            
            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower(); 
                newsQuery = newsQuery.Where(n =>
                    n.Title.ToLower().Contains(lowerSearchQuery) ||  
                    n.Content.ToLower().Contains(lowerSearchQuery)); 
            }

            if (startDate.HasValue)
            {
                newsQuery = newsQuery.Where(n => n.PublishDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                newsQuery = newsQuery.Where(n => n.PublishDate <= endDate.Value);
            }
            return await newsQuery.OrderByDescending(n => n.PublishDate).ToListAsync();
        }







        /// <summary>
        /// Hàm viết sử dụng cho việc lấy các vị trí quảng cáo
        /// </summary>
        /// <param name="position"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Lấy danh sách bài viết theo thể loại và quảng cáo 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
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



        /// <summary>
        /// hiển thị danh sách bài viết đang chờ duyệt , gồm lấy danh sách , duyệt và từ chối
        /// </summary>
        /// <returns></returns>
        public async Task<List<NewsViewModel>> GetAllPendingNewsAsync()
        {
            return await _context.News
                .Where(n => !n.Status && n.ApprovalStatus == ApprovalStatus.Pending)
                .Include(n => n.NewsCategories)
                .Select(n => new NewsViewModel
                {
                    NewsId = n.NewsId,
                    Title = n.Title,
                    PublishDate = n.PublishDate,
                    CreatedByUserName = _context.Users.FirstOrDefault(u => u.UserID == n.CreatedByUserId).Name,
                    ListCategories = n.NewsCategories.Select(nc => nc.Category).ToList()
                })
                .ToListAsync();
        }

        /// <summary>
        /// Duyệt bài viết
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async Task<bool> ApproveNewsAsync(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            if (news == null)
            {
                return false;
            }

            news.Status = true;
            news.ApprovalStatus = ApprovalStatus.Approved;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Từ chối bài viết
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="rejectionReason"></param>
        /// <returns></returns>
        public async Task<bool> RejectNewsAsync(int newsId, string rejectionReason)
        {
            var news = await _context.News.FindAsync(newsId);
            if (news == null)
            {
                return false;
            }

            news.ApprovalStatus = ApprovalStatus.Rejected;
            news.RejectionReason = rejectionReason;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
