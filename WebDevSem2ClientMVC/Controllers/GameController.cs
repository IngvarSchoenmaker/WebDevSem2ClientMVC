using Microsoft.AspNetCore.Mvc;

namespace WebDevSem2ClientMVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using WebDevSem2ClientMVC.Areas.Identity.Data;
    using WebDevSem2ClientMVC.Models;

    [Authorize]
    public class GameController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;

        public GameController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44384/api/game/"); // Vervang dit door de juiste API-URL
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
            List<Game> games = await GetGamesFromApi();
            return View(games);
        }

        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Game(int gameId)
        {
            //game.Players = _userManager.Users;
            if (ModelState.IsValid)
            {
                await JoinGameInApi(gameId);
                return RedirectToAction($"Game/{gameId}");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            //game.Players = _userManager.Users;
            if (ModelState.IsValid)
            {
                await CreateGameInApi(game);
                return RedirectToAction("Index");
            }

            return View(game);
        }

        public async Task<IActionResult> Details(int id)
        {
            Game game = await GetGameFromApi(id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        private async Task<List<Game>> GetGamesFromApi()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("games");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(responseBody);

            return games;
        }

        private async Task CreateGameInApi(Game game)
        {
            string json = JsonConvert.SerializeObject(game);
            HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("games", content);
            response.EnsureSuccessStatusCode();
        }
        private async Task JoinGameInApi(int gameId)
        {
            var context = HttpContext;
            string username = context.User.Identity.Name;
            ApplicationUser user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                NotFound();
            }
            //HttpResponseMessage response = await _httpClient.PatchAsync($"games/player/{gameId}/{user.Id}");
            //response.EnsureSuccessStatusCode();
        }

        private async Task<Game> GetGameFromApi(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"games/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Game game = JsonConvert.DeserializeObject<Game>(responseBody);
                return game;
            }

            return null;
        }
    }

}
