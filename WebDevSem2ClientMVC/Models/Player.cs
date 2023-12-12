using WebDevSem2ClientMVC.Areas.Identity.Data;

namespace WebDevSem2ClientMVC.Models
{
    public class Player : ApplicationUser
    {
        public string Name { get; set; }
        public List<Card> HandCards { get; set; }
    }
}
