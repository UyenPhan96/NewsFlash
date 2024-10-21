using System.Globalization;
using Web_News.Models;

namespace Web_News.Areas.Admin.ServiceAd.DashboardSV
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public int GetNumberOfUsers()
        {
            var userRoleId = 2; // RoleId của User
            return _context.UserRoles
                .Where(ur => ur.RoleId == userRoleId && ur.User.IsDeleted == false)
                .Count();
        }

        public int GetNumberOfDisplayedNews()
        {
            return _context.News
                .Where(n => n.Status == true && n.ApprovalStatus == ApprovalStatus.Approved)
                .Count();
        }

        public int GetNumberOfDisplayedAds()
        {
            return _context.Advertisements
                .Where(a => a.Status == Status.Displayed && a.EndDate >= DateTime.Now)
                .Count();
        }

        public int GetUserCountLastWeek()
        {
            var userRoleId = 2; // RoleId của User
            var currentWeekStart = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var lastWeekStart = currentWeekStart.AddDays(-7);
            var lastWeekEnd = currentWeekStart.AddTicks(-1); // Kết thúc của tuần trước

            return _context.UserRoles
                .Where(ur => ur.RoleId == userRoleId
                    && ur.User.IsDeleted == false
                    && ur.User.RegistrationDate >= lastWeekStart
                    && ur.User.RegistrationDate <= lastWeekEnd)
                .Count();
        }

        public int GetDisplayedNewsCountLastWeek()
        {
            var currentWeekStart = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var lastWeekStart = currentWeekStart.AddDays(-7);
            var lastWeekEnd = currentWeekStart.AddTicks(-1); // Kết thúc của tuần trước

            return _context.News
                .Where(n => n.Status == true
                    && n.ApprovalStatus == ApprovalStatus.Approved
                    && n.PublishDate >= lastWeekStart
                    && n.PublishDate <= lastWeekEnd)
                .Count();
        }

        public int GetDisplayedAdsCountLastWeek()
        {
            var currentWeekStart = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var lastWeekStart = currentWeekStart.AddDays(-7);
            var lastWeekEnd = currentWeekStart.AddTicks(-1); // Kết thúc của tuần trước

            return _context.Advertisements
                .Where(a => a.Status == Status.Displayed
                    && a.StartDate >= lastWeekStart
                    && a.StartDate <= lastWeekEnd
                    && a.EndDate >= DateTime.Now)
                .Count();
        }


        public string GetPercentageChange(int currentCount, int previousCount)
        {
            if (previousCount == 0)
            {
                return currentCount > 0 ? "Tăng 100%" : "Không thay đổi";
            }

            var percentageChange = ((currentCount - previousCount) / (double)previousCount) * 100;
            return percentageChange > 0
                ? $"Tăng {percentageChange:F2}%"
                : $"Giảm {-percentageChange:F2}%";
        }



        // Thêm phương thức này để lấy dữ liệu số lượng bài viết trong 10 ngày gần nhất  
        public Dictionary<string, int> GetNewsCountForLast7Days()
        {
            var newsCountByDay = new Dictionary<string, int>();

            // Lấy dữ liệu cho 7 ngày gần nhất (từ hôm nay lùi về trước 6 ngày)
            for (int i = 9; i >= 0; i--)
            {
                DateTime date = DateTime.Today.AddDays(-i);
                string formattedDate = date.ToString("dd/MM", CultureInfo.InvariantCulture);

                int newsCount = _context.News
                    .Where(n => n.PublishDate.Date == date.Date && n.Status == true && n.ApprovalStatus == ApprovalStatus.Approved)
                    .Count();
                newsCountByDay.Add(formattedDate, newsCount);
            }
            return newsCountByDay;
        }

        public Dictionary<string, int> GetNewsCountByAccountForCurrentMonth()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            // Lấy tất cả bài viết trong tháng hiện tại
            var newsInCurrentMonth = _context.News
                .Where(n => n.PublishDate.Month == currentMonth
                            && n.PublishDate.Year == currentYear
                            && n.Status == true
                            && n.ApprovalStatus == ApprovalStatus.Approved) // Lọc các bài đã duyệt
                .ToList();

            // Kiểm tra xem danh sách bài viết có trống không
            if (newsInCurrentMonth == null || !newsInCurrentMonth.Any())
            {
                throw new InvalidOperationException("No news found for the current month.");
            }

            // Lấy thông tin người dùng và số lượng bài viết của mỗi người trong tháng
            var result = newsInCurrentMonth
                .Join(_context.Users,
                      n => n.CreatedByUserId,
                      u => u.UserID,
                      (n, u) => new { News = n, User = u }) // Join để lấy thông tin người dùng
                .GroupBy(nu => nu.User.Name) // Nhóm theo tên tài khoản
                .ToDictionary(g => g.Key, g => g.Count()); // Đếm số bài viết cho mỗi người

            return result;
        }
        // Thêm phương thức này để lấy dữ liệu số lượng quảng cáo trong 10 ngày gần nhất  
        public Dictionary<string, int> GetAdsCountForLast10Days()
        {
            var adsCountByDay = new Dictionary<string, int>();

            // Lấy dữ liệu cho 10 ngày gần nhất (từ hôm nay lùi về trước 9 ngày)
            for (int i = 9; i >= 0; i--)
            {
                DateTime date = DateTime.Today.AddDays(-i);
                string formattedDate = date.ToString("dd/MM", CultureInfo.InvariantCulture);

                int adsCount = _context.Advertisements
                    .Where(a => a.StartDate.Date <= date.Date && a.EndDate.Date >= date.Date && a.Status == Status.Displayed)
                    .Count();

                adsCountByDay.Add(formattedDate, adsCount);
            }

            return adsCountByDay;
        }



    }
}
