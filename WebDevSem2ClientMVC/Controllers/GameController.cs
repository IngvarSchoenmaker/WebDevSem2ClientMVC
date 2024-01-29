using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using WebDevSem2ClientMVC.Hubs;
using WebDevSem2ClientMVC.Interfaces;
using WebDevSem2ClientMVC.Models;
using WebDevSem2ClientMVC.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text;

namespace WebDevSem2ClientMVC.Controllers
{
    public class GameController : Controller, IGameController
    {
        //check of dit nodig is of dat het uit player gehaald kan worden
        private int _gameId;
        private int _playerId;
        private List<Card> _playerHand;

        private UnoGame _unoGame;
        private readonly IHttpClientManager _httpClientManager;
        private readonly IHubContext<LobbyHub> _hubContext;


        public GameController(IHubContext<LobbyHub> hubContext, IHttpClientManager httpClientManager)
        {
            _hubContext = hubContext;
            _httpClientManager = httpClientManager;
        }
        public async Task<IActionResult> Index(int tableId, int playerId)
        {
            _gameId = tableId;
            _playerId = playerId;
            HttpResponseMessage response = await _httpClientManager.GetAsync($"getGameState/{_gameId}");
            if (response.IsSuccessStatusCode)
            {
                _unoGame = JsonConvert.DeserializeObject<UnoGame>(response.Content.ReadAsStringAsync().Result);
                _unoGame.You = _unoGame.Players.Find(player => player.PlayerId == _playerId);
                return View(_unoGame);
            }
            return BadRequest("Unable to join the table");
        }

        public async Task<IActionResult> StartGame(int TableId)
        {
            // Roept de API aan om het spel te starten
            HttpResponseMessage response = await _httpClientManager.PutAsync($"startGame/{TableId}");
            response.EnsureSuccessStatusCode();

            // Update dan de _unoGame met de ontvangen status
            response = await _httpClientManager.GetAsync($"GetGameState/{TableId}");
            response.EnsureSuccessStatusCode();

            // Todo: convert response naar Model UnoGame

            //get player hand
            response = await _httpClientManager.GetAsync($"getHand/{TableId}/{_playerId}");
            response.EnsureSuccessStatusCode();

            // _playerHand = response.Content.ReadAsStringAsync().Result; //ofzo


            // Mogelijk moet je enkele controles toevoegen om ervoor te zorgen dat de acties worden uitgevoerd op het juiste moment
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PlayCard(int TableId, Card playedCard)
        {
            // Roept de API aan om een kaart te spelen
            var jsonContent = new StringContent(JsonConvert.SerializeObject(playedCard), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClientManager.PostAsync($"PlayCard/{TableId}/{_playerId}", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                int index = _playerHand.FindIndex(card => card.Equals(playedCard));
                _playerHand.RemoveAt(index);
            }

            // Update dan de _unoGame met de ontvangen status
            // Mogelijk moet je enkele controles toevoegen om ervoor te zorgen dat de acties worden uitgevoerd op het juiste moment
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetCurrentCard(int TableId)
        {
            // Roept de API aan om een kaart te spelen

            HttpResponseMessage response = await _httpClientManager.GetAsync($"PlayCard/{TableId}");
            if (response.IsSuccessStatusCode)
            {

            }

            // Update dan de _unoGame met de ontvangen status
            // Mogelijk moet je enkele controles toevoegen om ervoor te zorgen dat de acties worden uitgevoerd op het juiste moment
            return RedirectToAction("Index");
        }
    }
}
