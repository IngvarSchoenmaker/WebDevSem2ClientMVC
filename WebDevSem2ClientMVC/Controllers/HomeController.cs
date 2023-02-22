using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2ClientMVC.Controllers
{
    public class HomeController : Controller
    {
        private DeveloperProfile? Profile { get; set; }

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
            return View(Profile);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}