using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2ClientMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DeveloperProfile profile { get; set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            List<string> skills = new()
            {
                "C#",
                "JS",
                "Python"
            };
            profile = new("Ingvar Schoenmaker", skills, "Mijn naam is Ingvar Schoenmaker en ik ben 23 jaar oud.\r\n Ik volg de opleiding HBO-ICT en heb de richting ontwikkeling gekozen.\r\n Momenteel zit ik in mijn laatste jaar maar heb voor een reparatiesimester gekozen.\r\n", "https://media.licdn.com/dms/image/C5603AQGU4RhjRZQnxg/profile-displayphoto-shrink_800_800/0/1517319432686?e=1681344000&v=beta&t=F6_63VvXX5m6Vu3q0UfMy89AnpCZuCnPyTM64UleLs8", "ingvar.schoenmaker@windesheim.nl");

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult DeveloperProfile()
        {
            return View(profile);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}