using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebDevSem2ClientMVC.Hubs;
using WebDevSem2ClientMVC.Interfaces;
using WebDevSem2ClientMVC.Models;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebDevSem2ClientMVC.Controllers
{
    public class LobbyTableController : Controller
    {
        private readonly IHttpClientManager _httpClientManager;
        private readonly IHubContext<LobbyHub> _hubContext;

        public LobbyTableController(IHubContext<LobbyHub> hubContext, IHttpClientManager httpClientManager)
        {
            _hubContext = hubContext;
            _httpClientManager = httpClientManager;
        }
        public async Task<IActionResult> Index()
        {
            // Hier zou je een lijst met beschikbare tafels uit een service kunnen halen
            var availableTables = await GetAvailableTables();
            return View(availableTables);
        }
        public async Task<IActionResult> JoinTable(int tableId)
        {
            // Voeg logica toe om de speler aan de tafel toe te voegen
            var response = await _httpClientManager.PostAsync($"joinTable/{tableId}/", null);

            if (response.IsSuccessStatusCode)
            {
                //await UpdateTableList(); // Update de lijst met tafels voor andere clients
                var playerId = await response.Content.ReadAsStringAsync();

                //Roep hier hub aan om de speler toe te voegen aan de groep

                return RedirectToAction("Index", "Game", new{ TableId = tableId, PlayerId = playerId});
            }

            return BadRequest("Unable to join the table");
        }

        private async Task UpdateTableList()
        {
            var availableTables = await GetAvailableTables();
            await _hubContext.Clients.All.SendAsync("UpdateTableList", availableTables);
        }
        public async Task<List<LobbyTable>>? GetAvailableTables()
        {

            HttpResponseMessage response = await _httpClientManager.GetAsync("getTables");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            List<LobbyTable> lobbyTables = JsonConvert.DeserializeObject<List<LobbyTable>>(responseBody);

            return lobbyTables;
        }
        [HttpGet]
        public IActionResult CreateTable()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable(LobbyTable model)
        {
            if (!ModelState.IsValid)
            {
                // Als het een AJAX-aanvraag is, retourneer JSON met foutmeldingen
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return BadRequest(ModelState);
                }

                // Als het geen AJAX-aanvraag is, retourneer de standaard weergave met fouten
                return View(model);
            }

            // Converteer het object naar JSON-formaat
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClientManager.PostAsync("createTable", jsonContent);
            Console.WriteLine(response);
            string theId = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                model.TableId = Int32.Parse(theId);
                await _hubContext.Clients.All.SendAsync("TableCreated", model);
                return await JoinTable(model.TableId);
                //return new RedirectResult(url: "/GameController/Index", permanent: true, preserveMethod: true);
            }
            return BadRequest($"Invalid input or connection problem");
        }
    }

}
