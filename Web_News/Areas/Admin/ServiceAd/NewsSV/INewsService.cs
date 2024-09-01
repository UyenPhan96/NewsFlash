﻿using Web_News.Areas.Admin.ViewModels;
using Web_News.Models;

namespace Web_News.Areas.Admin.ServiceAd.NewsSV
{
    public interface INewsService
    {
        Task<News> CreateNewsAsync(News news, IEnumerable<int> categoryIds);
        Task<News> UpdateNewsAsync(News news, IEnumerable<int> categoryIds);
        Task DeleteNewsAsync(int newsId);
        Task<NewsViewModel> GetNewsByIdAsync(int newsId);
        Task<IEnumerable<NewsViewModel>> GetAllNewsAsync();
    }
}
