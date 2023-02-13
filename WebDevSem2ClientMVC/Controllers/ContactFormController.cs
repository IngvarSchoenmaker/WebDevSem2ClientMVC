using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2ClientMVC.Controllers
{
    public class ContactFormController : Controller
    {
        private readonly GoogleCaptchaService _captchaService;

        public ContactFormController(GoogleCaptchaService captchaService) 
        { 
            _captchaService = captchaService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FormSuccess()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormModel model)
        {
            var captchaResult = await _captchaService.VerifyToken(model.Token);
            if (!captchaResult)
            {
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            //Verify token with google

            return new RedirectResult(url: "/ContactForm/FormSuccess", permanent: true, preserveMethod: true);
        }
    }
}
