using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2ClientMVC.Hubs
{
    public class LobbyHub : Hub
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
        public async Task SendTableCreatedUpdate(LobbyTable newTable)
        {
            // Stuur de nieuwe tafel naar alle clients
            await Clients.All.SendAsync("TableCreated", newTable);
        }
    }
}