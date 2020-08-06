using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Img_socialmedia.Hubs
{
    public class NotificationHub: Hub
    {
        public async Task Notification(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}