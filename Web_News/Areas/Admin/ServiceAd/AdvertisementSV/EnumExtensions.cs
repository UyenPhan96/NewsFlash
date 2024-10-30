using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
namespace Web_News.Areas.Admin.ServiceAd.AdvertisementSV
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var enumMember = enumValue.GetType().GetMember(enumValue.ToString()).First();
            var displayAttribute = enumMember.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.GetName() ?? enumValue.ToString();
        }
        public static string TimeAgo(DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalSeconds < 60)
                return $"{(int)timeSpan.TotalSeconds} giây trước";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} phút trước";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} giờ trước";
            if (timeSpan.TotalDays < 30)
                return $"{(int)timeSpan.TotalDays} ngày trước";
            if (timeSpan.TotalDays < 365)
                return $"{(int)(timeSpan.TotalDays / 30)} tháng trước";

            return $"{(int)(timeSpan.TotalDays / 365)} năm trước";
        }
    }
}
