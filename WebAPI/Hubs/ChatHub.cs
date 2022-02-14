using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebAPI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            var connectionId = Context.ConnectionId;
            await Clients.Others.SendAsync("ReceiveMessage",
                $"{connectionId}: {message}");
        }
    }
}
