using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2API.Controllers
{
    public interface IUnoRepository
    {
        public Task<IEnumerable<Game>> GetGames();
        public Task<Game> GetGame(int id);
        public Task<Game> GetGame(string id);
        public Task<Game> CreateGame(Game game);
        public Task<Game> PatchGame(int gameId);
        public void DeleteGame(int gameId);
        public void AddPlayer(int gameId, string id);
        public void RemovePlayer(string id);
        public Task<IEnumerable<Card>> GetPlayerHand(string id);
        public Task<Card> GetCard(int cardId);
        public Task<Card> GetRandomCard(string id);
        public void RemoveCard(string id, int cardId);
        public void AddCardHand(Card card, string id);

    }
}