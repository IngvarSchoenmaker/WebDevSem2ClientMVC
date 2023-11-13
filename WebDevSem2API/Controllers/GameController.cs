using Microsoft.AspNetCore.Mvc;
using WebDevSem2ClientMVC.Areas.Identity.Data;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IUnoRepository _repository;

        public GameController(IUnoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/games
        [HttpGet("games")]
        public async Task<ActionResult<IEnumerable<Game>>> GamesGet()
        {
            var games = await _repository.GetGames();
            if (games == null)
            {
                return NotFound();
            }
            return Ok(games);
        }

        // GET: api/games/id
        [HttpGet("games/{id}")]
        public async Task<ActionResult<Game>> GameGet(int gameId)
        {
            var game = await _repository.GetGame(gameId);
            if (game == null)
            {
                return NotFound();
            }
            return game;
        }

        // POST: api/games
        [HttpPost("games")]
        //Task weghalen als het stuk gaat
        public async Task<ActionResult> GameCreate([FromBody] Game game)
        {
            if (game == null)
            {
                return BadRequest();
            }
            await _repository.CreateGame(game);
            return CreatedAtAction(nameof(GameGet), new { id = game.GameId }, game);
        }

        // DELETE: api/games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> GameDelete(int gameId)
        {
            try
            {
                _repository.DeleteGame(gameId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        // PATCH: api/games/id
        [HttpPatch("games/{id}")]
        public async Task<ActionResult<Card>> GameChangeCard(int gameId)
        {
            var game = await _repository.GetGame(gameId);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game.Cards);
        }

        // Patch: api/games/player/5/6
        [HttpPatch("games/player/{gameid}/{playerid}")]
        public async Task<ActionResult> PlayerAddToGame(int gameId, string playerId)
        {
            if (gameId == null || playerId == null)
            {
                return BadRequest();
            }
            _repository.AddPlayer(gameId, playerId);
            return Ok();
        }

        // Patch: api/games/player/5
        [HttpPatch("games/player/{id}")]
        public async Task<ActionResult> PlayerRemoveFromGame(string playerId)
        {
            if (playerId == null)
            {
                return BadRequest();
            }
            _repository.RemovePlayer(playerId);
            return Ok();
        }


        // GET: api/games/id
        [HttpGet("games/player/hand/{id}")]
        public async Task<ActionResult<Game>> PlayerGetHand(string playerId)
        {
            if (playerId == null)
            {
                return BadRequest();
            }
            var playerHand = await _repository.GetPlayerHand(playerId);
            if (playerHand == null)
            {
                return NotFound();
            }
            return Ok(playerHand);
        }

        // PATCH: api/games/id
        [HttpPatch("games/player/hand/{playerid}/{cardid}")]
        public async Task<ActionResult<Game>> PlayerPlayCard(string playerId, int cardId)
        {
            if (playerId == null)
            {
                return BadRequest();
            }
            var game = await _repository.GetGame(playerId);
            if (game == null)
            {
                return NotFound();
            }
            var card = await _repository.GetCard(cardId);
            if (card == null)
            {
                return NotFound();
            }

            if (game.CurrentCard == card)
            {
                game.CurrentCard = card;
                _repository.RemoveCard(playerId, cardId);
            }
            else
            {
                return BadRequest("Deze kaart mag niet gespeeld worden");
            }

            return Ok();
        }

        // GET: api/games/hand/card
        [HttpGet("games/player/hand/card")]
        public async Task<ActionResult<IEnumerable<Card>>> PlayerGetCard(string playerId)
        {
            if (playerId == null)
            {
                return BadRequest();
            }
            var randomCard = await _repository.GetRandomCard(playerId);
            if (randomCard == null)
            {
                return NotFound("No random card found");
            }
            _repository.AddCardHand(randomCard, playerId);
            return Ok(_repository.GetPlayerHand);
        }

    }
}
