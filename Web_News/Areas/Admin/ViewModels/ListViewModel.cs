﻿using Web_News.Models;

namespace Web_News.Areas.Admin.ViewModels
{
    public class ListViewModel
    {
        public List<News> LatestNews { get; set; } // Danh sách bài viết
        public List<Advertisement> SidebarAdvertisements { get; set; } // Quảng cáo Sidebar
        public List<Advertisement> HeaderAdvertisements { get; set; } // Quảng cáo Header
        public List<Advertisement> FooterAdvertisements { get; set; } // Quảng cáo Footer
        public List<Category> Categories { get; set; } // Danh sách danh mục
        public int? SelectedCategoryId { get; set; } // ID của danh mục được chọn

        // Phần trang 
        // Phân trang
        public int PageNumber { get; set; } // Trang hiện tại
        public int PageSize { get; set; } // Số lượng bài viết mỗi trang
        public int TotalItems { get; set; } // Tổng số bài viết
    }
}
