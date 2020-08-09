using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Img_socialmedia.Hubs
{
    public class Notification : Hub
    {
        public async Task send(int userid, string mess)
        {
            await Clients.All.SendAsync("new mess", userid , mess);
        }
    }
}