﻿using Microsoft.AspNetCore.Mvc;
using WebDevSem2ClientMVC.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Http.Json;
using System.Net.Http;
using NuGet.Protocol;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebDevSem2ClientMVC.Areas.Identity.Data;

namespace WebDevSem2ClientMVC.Controllers
{
    public class ContactFormController : Controller
    {
        private readonly GoogleCaptchaService _captchaService;
        private readonly IConfiguration Configuration;

        public IHttpClientFactory _httpClientFactory { get; set; }

        public ContactFormController(GoogleCaptchaService captchaService, IConfiguration configuration, IHttpClientFactory httpClientFactory) 
        {
            _captchaService = captchaService;
            Configuration = configuration;
            _httpClientFactory = httpClientFactory;
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
        public async Task<IActionResult> Index(ContactForm model)
        {
            //Verify token with google
            var captchaResult = await _captchaService.VerifyToken(model.Token!);
            if (!captchaResult)
            {
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }


            var client = _httpClientFactory.CreateClient("localhost");

            try
            {
                await client.PostAsJsonAsync("ContactFormModels", model);
            }
            catch (Exception)
            {

                throw;
            }


            //Send mail
            Execute(model).Wait();
            

            //var url = new Uri();
            //using var client = new HttpClient();
            //client.BaseAddress = new Uri(url);

            return new RedirectResult(url: "/ContactForm/FormSuccess", permanent: true, preserveMethod: true);
        }

        static async Task Execute(ContactForm model)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ingvar.schoenmaker@windesheim.nl", "Ingvar Schoenmaker");
            var subject = model.Subject;
            var to = new EmailAddress(model.Email, model.Email);
            var plainTextContent = model.Message;
            var htmlContent = $"<strong>{model.Message}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
