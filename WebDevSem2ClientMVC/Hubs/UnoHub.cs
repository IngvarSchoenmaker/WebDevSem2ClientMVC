using Microsoft.AspNetCore.SignalR;

namespace WebDevSem2ClientMVC.Hubs
{
    public class UnoHub : Hub
    {
        public async Task PlayerJoined(string playerName)
        {
            await Clients.All.SendAsync("PlayerJoined", playerName);
        }

        public async Task GameStarted(IEnumerable<string> playerNames)
        {
            await Clients.All.SendAsync("GameStarted", playerNames);
        }

        public async Task CardPlayed(string playerId, string card)
        {
            await Clients.All.SendAsync("CardPlayed", playerId, card);
        }
        public async Task NewTableCreated(string tableName)
        {
            await Clients.All.SendAsync("TableCreated", tableName);
        }
    }
}