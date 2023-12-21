using Microsoft.AspNetCore.Identity;
using WebDevSem2ClientMVC.Areas.Identity.Data;

namespace WebDevSem2ClientMVC.Models
{
    public class Player
    {
        public int Id { get; set; }
        public List<Card>? HandCards { get; set; }
        public UnoGame? Game { get; set; }
    }
}
