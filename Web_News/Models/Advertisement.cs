using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_News.Models
{
    public class Advertisement
    {
        [Key]
        public int AdvertisementId { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactName { get; set; }
        [MaxLength]
        public string Content { get; set; }

        [MaxLength]
        public string Image { get; set; }

        [Url]
        public string Link { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [EnumDataType(typeof(BannerPosition))]
        public BannerPosition BannerPosition { get; set; }

        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        [EnumDataType(typeof(ApprovalStatus))]
        public ApprovalStatus ApprovalStatus { get; set; }

        public int? ApprovedByUserId { get; set; }

        [ForeignKey("ApprovedByUserId")]
        public User? ApprovedByUser { get; set; }
    }
    public enum BannerPosition
    {
        None,
        Header,
        Sidebar,
        Footer,
        HomePageTop,
        HomePageBottom
    }

    public enum Status
    {
        [Display(Name = "Ẩn")]
        Hidden,   // Ẩn
        [Display(Name = "Hiển Thị")]
        Displayed, // Hiển thị
        [Display(Name = "Hết Hạn")]
        Expired   // Hết hạn
    }

    public enum ApprovalStatus
    {
        [Display(Name = "Đang chờ duyệt")]
        Pending,   
        [Display(Name = "Duyệt")]
        Approved,  
        [Display(Name = "Từ Chối")]
        Rejected   
    }

}
