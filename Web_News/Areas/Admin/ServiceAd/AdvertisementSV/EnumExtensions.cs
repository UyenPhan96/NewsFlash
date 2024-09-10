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
    }
}
