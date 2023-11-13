using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using WebDevSem2ClientMVC.Areas.Identity.Data;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2API.Controllers
{
    public class UnoRepository : ControllerBase, IUnoRepository
    {
        private readonly ApplicationDBContext _context;

        public UnoRepository(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Game>> GetGames() => await _context.Game.ToListAsync();

        public async Task<Game> GetGame(int id) => await _context.Game.FindAsync(id);
        public async Task<Game> GetGame(string id)
        {
            var user = _context.Player.FirstOrDefault(x => x.Id == id);
            return await _context.Game.FindAsync(user);
        }

        public async Task<Game> CreateGame(Game game)
        {
            _context.Game.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<Game> PatchGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public void DeleteGame(int gameId)
        {
            var game = _context.Game.FirstOrDefault(g => g.GameId == gameId);
            if (game == null) 
            { 
                throw new ArgumentException("Game met het opgegeven gameId kon niet worden gevonden.");
            }
            foreach (var player in game.Players)
            {
                player.GameId = null;
            }
            _context.Game.Remove(game);
            _context.SaveChanges();
        }

        public async void RemovePlayer(string id)
        {
            var player = await _context.Player.FirstOrDefaultAsync(p => p.Id == id);
            if (player != null)
            {
                player.GameId = null;
            }
        }

        public void AddPlayer(int gameId, string id)
        {
            var game = _context.Game.FirstOrDefault(g => g.GameId == gameId);
            var user = _context.Player.FirstOrDefault(x => x.Id == id);
            if (game == null)
            {
                throw new ArgumentException("Game met het opgegeven gameId kon niet worden gevonden.");
            }
            if (user == null)
            {
                throw new ArgumentException("User met het opgegeven userId kon niet worden gevonden.");
            }

            // Controleer de game-status en andere vereisten voordat een speler wordt toegevoegd
            if (game.GameStatus == GameStatus.Started && game.Players.Count > 1) // dit kan je naar meer veranderen als dit nodig is
            {
                throw new InvalidOperationException("Speler kan niet aan de game worden toegevoegd. Gamestatus is niet geschikt of het maximumaantal spelers is bereikt.");
            }

            user.GameId = gameId;
            game.Players.Add(user);

            _context.SaveChanges();

        }

        public async Task<IEnumerable<Card>> GetPlayerHand(string id)
        {
            var card = await _context.Cards.Where(c => c.Id == id).Select(c => c.Card).ToListAsync();
            return card;
        }

        public async Task<Card> GetCard(int cardId) => await _context.Card.FirstOrDefaultAsync(x => x.CardId == cardId);
        public async Task<Card> GetRandomCard(string id)
        {
            Random r = new Random();
            int rInt = r.Next(1, 37); // number between 1 and 36 (4 color times 9 numbers)
            return await _context.Card.FirstOrDefaultAsync(x => x.CardId == rInt);
        }

        public void RemoveCard(string id, int cardId)
        {
            var card = _context.Cards.FirstOrDefault(c => c.Id == id && c.CardId == cardId);
            if (card != null)
            {
                _context.Cards.Remove(card);
                _context.SaveChanges();
            }
        }

        public void AddCardHand(Card card, string id)
        {
            int gameId = GetGame(id).Id;
            _context.Cards.Add(new Cards {GameId=gameId, Id = id, CardId = card.CardId });
        }

        public Task<Game> GetGameFormPlayer(string id)
        {

            throw new NotImplementedException();
        }

    }
}
