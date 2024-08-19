using Microsoft.AspNetCore.SignalR;

namespace CentralAPI.Model
{
    public class SampleHub : Hub
    {
        public async Task SendMessage(string name, DateTime createdAt)
        {
            await Clients.All.SendAsync("ReceiveMessage", new { name, createdAt });
        }
    }
}
