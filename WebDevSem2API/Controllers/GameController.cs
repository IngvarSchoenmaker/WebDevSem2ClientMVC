using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using NuGet.Protocol.Core.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDevSem2ClientMVC.Areas.Identity.Data;
using WebDevSem2ClientMVC.Hubs;
using WebDevSem2ClientMVC.Models;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IHubContext<UnoHub> _hubContext;
    private readonly ApplicationDBContext _dbContext;

    private static List<Player> _players = new List<Player>();
    private static UnoGame _unoGame;

    public GameController(IHubContext<UnoHub> hubContext, ApplicationDBContext dbContext)
    {
        _hubContext = hubContext;
        _dbContext = dbContext;
    }

    [HttpPost("joinGame")]
    public async Task<IActionResult> JoinGame(string playerName)
    {
        var player = new Player { Id = HttpContext.Connection.Id };
        _players.Add(player);

        await _hubContext.Clients.All.SendAsync("PlayerJoined", playerName);
        await UpdatePlayers();

        return Ok(new { PlayerId = player.Id });
    }


    [HttpPost("startGame")]
    public async Task<IActionResult> StartGame()
    {
        if (_players.Count < 2)
        {
            return BadRequest("Cannot start the game with fewer than 2 players.");
        }

        _unoGame = new UnoGame(_players);

        await _hubContext.Clients.All.SendAsync("GameStarted", _players.Select(p => p.Name));
        await UpdatePlayers();

        return Ok("Game started successfully");
    }

    [HttpPost("playCard")]
    public async Task<IActionResult> PlayCard(string playerId, Card playedCard)
    {
        if (_unoGame == null)
        {
            return BadRequest("The game has not started yet.");
        }

        var result = _unoGame.PlayCard(playerId, playedCard);

        if (result != null)
        {
            await _hubContext.Clients.All.SendAsync("CardPlayed", playerId, result);
            await UpdatePlayers();

            return Ok("Card played successfully");
        }

        return BadRequest("Invalid move. The played card is not valid.");
    }

    [HttpPost("createTable")]
    public async Task<IActionResult> CreateTable([FromBody] LobbyTable request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _dbContext.LobbyTable.Add(request);
        await _dbContext.SaveChangesAsync();

        // Stuur een update naar clients via SignalR
        await _hubContext.Clients.All.SendAsync("TableCreated", request.TableName);

        return Ok("Table created successfully");
    }


    [HttpGet("getTables")]
    public IActionResult GetTables()
    {
        var tables = _dbContext.LobbyTable.ToList();
        return Ok(tables);
    }

    private async Task UpdatePlayers()
    {
        await _hubContext.Clients.All.SendAsync("UpdatePlayers", _players);
    }
}
