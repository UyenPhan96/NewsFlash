using Web_News.Models;

namespace Web_News.Areas.Admin.ViewModels
{
    public class ListViewModel
    {
        public List<News> LatestNews { get; set; } // Danh sách đầu 
        public List<News> News360 { get; set; }
        public List<News> RecommendedNews { get; set; }
        public List<News> WorldNews { get; set; }
        public List<News> EconomyNews { get; set; } // Kinh tế
        public List<News> TechnologyNews { get; set; } // Công nghệ
        public List<News> CultureNews { get; set; } // Văn hóa
        public List<News> SportsNews { get; set; } // Thể thao
        public List<News> LawNews { get; set; } // Pháp luật
        public List<News> TrafficNews { get; set; } // Giao thông
        public List<Advertisement> SidebarAdvertisements { get; set; } 
        public List<Advertisement> HeaderAdvertisements { get; set; } 
        public List<Advertisement> FooterAdvertisements { get; set; } 
        public List<Category> Categories { get; set; } 
        public int? SelectedCategoryId { get; set; }


        // Phân trang
        public int PageNumber { get; set; } // Trang hiện tại
        public int PageSize { get; set; } // Số lượng bài viết mỗi trang
        public int TotalItems { get; set; } // Tổng số bài viết
    }
}
