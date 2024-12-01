namespace Web_News.Areas.Admin.ViewModels
{
    public class NotificationViewModel
    {
        public int AdvertisementId { get; set; }
        public string ContactName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }
    }
}
