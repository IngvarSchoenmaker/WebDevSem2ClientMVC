using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using WebDevSem2ClientMVC.Areas.Identity.Data;
using WebDevSem2ClientMVC.Hubs;
using WebDevSem2ClientMVC.Migrations;
using WebDevSem2ClientMVC.Models;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly ApplicationDBContext _dbContext;

    //private UserManager<Player> _userManager;
    public GameController(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
        //_userManager = usermanager;
    }
    [HttpPost("createTable")]
    public async Task<IActionResult> CreateTable([FromBody] LobbyTable request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        UnoGame? unoGame = new UnoGame();
        request.Game = unoGame;
        var data = _dbContext.LobbyTable.Add(request);

        await _dbContext.SaveChangesAsync();

        request.TableId = data.Entity.TableId;

        return Ok(data.Entity.TableId);

    }
    [HttpPost("joinTable/{tableId}")]
    public async Task<IActionResult> JoinTable(int tableId)
    {        
        Player? player = new Player();
        var data = await _dbContext.Player.AddAsync(player);
        await _dbContext.SaveChangesAsync();
        player.Id = data.Entity.Id;
        //Find table by id
        LobbyTable? LobbyTable = _dbContext.LobbyTable.Find(tableId);
        //Check if found
        if (LobbyTable == null)
        {
            return BadRequest("Table id not found.");
        }
        UnoGame? unogame = LobbyTable.Game; // TODO: check get uno game?
        //Check if table got game
        if (unogame == null)
        {
            //create game if not
            unogame = new UnoGame(player);
            LobbyTable.Game = unogame;
        }
        else
        {
            //join game if yes
            unogame.JoinGame(player);
        }

        if (unogame != null)
        {
            var databaseGame = await _dbContext.UnoGame.AddAsync(unogame);
            await _dbContext.SaveChangesAsync();
            player.Game = databaseGame.Entity;

            return Ok(player.Id);
        }

        return BadRequest("Invalid move. The played card is not valid.");
    }
    [HttpPut("startGame")]
    public async Task<IActionResult> StartGame(int gameId)
    {
        UnoGame? unoGame = GetUnoGame(gameId);
        if (unoGame != null && unoGame.GameStatus == GameStatus.WaitingForPlayers)
        {
            unoGame.ChangeGameStatus();
            _dbContext.Entry(unoGame).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
                return Ok("Game started successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        return BadRequest("Game start problems");
    }

    [HttpPost("playCard/{gameId}/{playerId}")]
    public IActionResult PlayCard(int gameId, string playerId, Card playedCard)
    {
        UnoGame? unoGame = GetUnoGame(gameId);
        if (unoGame == null)
        {
            return BadRequest("Game not found");
        }

        if (unoGame == null || unoGame.GameStatus != GameStatus.InProgress)
        {
            return BadRequest("The game has not started yet.");
        }

        var result = unoGame.PlayCard(playerId, playedCard);

        if (result != null)
        {
            return Ok("Card played successfully");
        }

        return BadRequest("Invalid move. The played card is not valid.");
    }

    [HttpGet("getGameState/{gameId}")]
    public IActionResult GetGameState(int gameId)
    {
        UnoGame? unoGame = GetUnoGame(gameId);

        if (unoGame != null)
        {
            return Ok(unoGame); // Je moet mogelijk een aangepaste DTO maken voor de gamestatus
        }

        return NotFound("Game not found.");
    }
    [HttpGet("getHand/{gameId}/{playerId}")]
    public IActionResult GetHand(int gameId, int playerId)
    {
        UnoGame? unoGame = GetUnoGame(gameId);

        if (unoGame != null && unoGame.Players[playerId] != null)
        {
            List<Card> hand = unoGame.GetStartingHand();
            unoGame.Players[playerId].HandCards = hand;
            return Ok(unoGame); // Je moet mogelijk een aangepaste DTO maken voor de gamestatus
        }

        return NotFound("Game or player not found.");
    }
    // Find the game by id
    private UnoGame? GetUnoGame(int gameId)
    {
        return _dbContext.UnoGame.Find(gameId);
    }


    [HttpGet("getTables")]
    public IActionResult GetTables()
    {
        List<LobbyTable>? tables = _dbContext.LobbyTable.ToList();
        return Ok(tables);
    }
}
