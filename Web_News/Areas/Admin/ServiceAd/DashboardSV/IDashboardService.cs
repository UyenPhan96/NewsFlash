
namespace Web_News.Areas.Admin.ServiceAd.DashboardSV
{
    public interface IDashboardService
    {
        int GetNumberOfUsers();
        int GetNumberOfDisplayedNews();
        int GetNumberOfDisplayedAds();
        Dictionary<string, int> GetNewsCountForLast7Days();
        string GetPercentageChange(int currentCount, int previousCount);
        int GetDisplayedAdsCountLastWeek();
        int GetDisplayedNewsCountLastWeek();
        int GetUserCountLastWeek();
        Dictionary<string, int> GetNewsCountByAccountForCurrentMonth();
        Dictionary<string, int> GetAdsCountForLast10Days();
    }
}
