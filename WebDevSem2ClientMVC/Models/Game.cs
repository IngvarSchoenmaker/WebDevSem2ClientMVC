using System.ComponentModel.DataAnnotations;
using WebDevSem2ClientMVC.Areas.Identity.Data;

namespace WebDevSem2ClientMVC.Models
{
    public enum GameStatus
    {
        Waiting,
        Started
    }
    public class Game
    {
        public int GameId { get; set; }
        [Required]
        [StringLength(50)]
        public string GameName { get; set; }
        [Range(2, 10)]
        public int NumberOfPlayers { get; set; }
        [Required]
        public GameStatus GameStatus { get; set; }
        public Card? CurrentCard { get; set; }
        public virtual ICollection<ApplicationUser>? Players { get; set; }
        public virtual IEnumerable<Cards>? Cards { get; set; }

    }
}
