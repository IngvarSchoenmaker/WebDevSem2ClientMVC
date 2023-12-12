using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebDevSem2ClientMVC.Interfaces;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2ClientMVC.Controllers
{
    public class LobbyTableController : Controller
    {
        private readonly IHttpClientManager _httpClientManager;

        public LobbyTableController(IHttpClientManager httpClientManager)
        {
            _httpClientManager = httpClientManager;
        }
        public async Task<IActionResult> Index()
        {
            // Hier zou je een lijst met beschikbare tafels uit een service kunnen halen
            var availableTables = await GetAvailableTables();
            return View(availableTables);
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
            // check if table name is not empty
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

            if (response.IsSuccessStatusCode)
            {
                return Ok("Table created successfully");
            }
            return BadRequest($"Invalid input or connection problem");
        }
    }

}
