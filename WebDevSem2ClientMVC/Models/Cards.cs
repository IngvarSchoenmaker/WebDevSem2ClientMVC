using System.ComponentModel.DataAnnotations;
using WebDevSem2ClientMVC.Areas.Identity.Data;

namespace WebDevSem2ClientMVC.Models
{
    public class Cards
    {
        [Key]
        public int CardsId { get; set; }
        public int GameId { get; set; }
        //player id
        public string Id { get; set; }
        public int CardId { get; set; }
        public virtual Game Game { get; set; }
        public virtual ApplicationUser Player { get; set; }
        public virtual Card Card { get; set; }
    }
}
