namespace Web_News.Areas.Admin.ViewModels
{
    public class PageResult
    {
        public List<NewsViewModel> News { get; set; }
        public int TotalPages { get; set; }
    }
}
