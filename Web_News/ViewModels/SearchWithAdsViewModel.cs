using Web_News.Models;

namespace Web_News.ViewModels
{
    public class SearchWithAdsViewModel
    {
        // Danh sách các bài viết
        public List<News> LatestNews { get; set; }

        // Danh sách quảng cáo theo vị trí
        public List<Advertisement> SidebarAdvertisements { get; set; } // Quảng cáo Sidebar
        public List<Advertisement> HeaderAdvertisements { get; set; }  // Quảng cáo Header
        public List<Advertisement> FooterAdvertisements { get; set; }  // Quảng cáo Footer

        // Các thông tin liên quan đến tìm kiếm bài viết
        public string SearchQuery { get; set; }  // Từ khóa tìm kiếm
        public int CategoryId { get; set; }  // Thể loại
        public DateTime? StartDate { get; set; }  // Tìm theo ngày bắt đầu (optional)
        public DateTime? EndDate { get; set; }  // Tìm theo ngày kết thúc (optional)
        public List<Category> Categories { get; set; }

        // Thêm các thuộc tính cho phân trang
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
