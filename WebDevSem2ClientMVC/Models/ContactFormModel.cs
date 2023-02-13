using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using WebDevSem2ClientMVC.ViewModels;

namespace WebDevSem2ClientMVC.Models
{
    public class ContactFormModel : ContactFormViewModel
    {
        
        public int Id { get; set; }

        [Display(Name = "Developer")]
        public DeveloperProfile? DeveloperProfile { get; set; }

        [Required(ErrorMessage = "E-mail is verplicht")]
        [EmailAddress]
        [Display(Name = "E-mailadres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Titel is verplicht")]
        [Display(Name = "Titel")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "De {0} mag maximaal {1} lang zijn.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Bericht is verplicht")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "De titel mag maximaal 500 lang zijn.")]
        [Display(Name = "Bericht")]
        public string Message { get; set; }
    }
}
