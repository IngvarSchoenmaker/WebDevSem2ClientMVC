using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace WebDevSem2ClientMVC.Controllers
{
    public class Uno : Controller
    {
        public Bitmap UnoCards { get; set; }
        public Uno() 
        {
            UnoCards = new Bitmap("./img/collin-gale-uno-sheet.jpg");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetImage()
        {
            return View();
        }
    }
}
