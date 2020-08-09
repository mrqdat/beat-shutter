using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Img_socialmedia.Hubs
{
    public class Comment:Hub
    {
        public async Task Commentation( string content)
        {
            await Clients.All.SendAsync("recevie",content);  
        }
    }
}
