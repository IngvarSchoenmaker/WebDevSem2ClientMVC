using Microsoft.AspNetCore.Mvc;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2ClientMVC.Interfaces
{
    public interface IGameController
    {
        Task<IActionResult> GetCurrentCard(int TableId);
        Task<IActionResult> Index(int tableId, int playerId);
        Task<IActionResult> PlayCard(int TableId, Card playedCard);
        Task<IActionResult> StartGame(int TableId);
    }
}