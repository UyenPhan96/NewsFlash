using Microsoft.AspNetCore.SignalR;

namespace Web_News.Areas.Admin.Hubs
{
    public class NotificationHub : Hub
    {
        // Phương thức này sẽ gửi thông báo đến tất cả các client
        public async Task SendNotification()
        {
            await Clients.All.SendAsync("ReceiveNotification");
        }
    }
}
