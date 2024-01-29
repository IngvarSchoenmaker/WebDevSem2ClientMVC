using Microsoft.AspNetCore.SignalR; //deze
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using WebDevSem2ClientMVC.Interfaces;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2ClientMVC.Hubs
{
    public class LobbyHub : Hub
    {
        private readonly IHttpClientManager _httpClientManager;

        public LobbyHub(IHttpClientManager httpClientManager)
        {
            _httpClientManager = httpClientManager;
        }
        public async Task GameStarted(IEnumerable<string> playerNames)
        {
            await Clients.All.SendAsync("GameStarted", playerNames);
        }
        public async Task CardPlayed(string playerId, string card)
        {
            await Clients.All.SendAsync("CardPlayed", playerId, card);
        }
        public async Task JoinGroup(string tableId)
        {
            // Stuur de nieuwe tafel naar alle clients
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Table-{tableId}");
            await Clients.Group($"Table-{tableId}").SendAsync($"PlayerJoined");
        }
        public async Task PlayerJoined(string tableId)
        {
            var response = await _httpClientManager.GetAsync($"getGameState/{tableId}");
            if (response.IsSuccessStatusCode)
            {
                var unoGame = JsonConvert.DeserializeObject<UnoGame>(response.Content.ReadAsStringAsync().Result);
                await Clients.Group($"Table-{tableId}").SendAsync("AddPlayerToList", unoGame.Players);
            }
        }
        public async Task SendTableCreatedUpdate(LobbyTable newTable)
        {
            // Stuur de nieuwe tafel naar alle clients
            await Clients.All.SendAsync("TableCreated", newTable);
        }
        public async Task RemovePlayerFromList(string playerId)
        {
            // Stuur de nieuwe tafel naar alle clients
            await Clients.All.SendAsync("TableCreated", playerId);
        }
    }
}