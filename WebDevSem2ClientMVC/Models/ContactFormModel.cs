using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebDevSem2ClientMVC.ViewModels;

namespace WebDevSem2ClientMVC.Models
{
    public class ContactFormModel : ContactFormViewModel
    {
        
        [Required]
        [Key]
        public int ContactFormId { get; set; }

        [Required]
        [ForeignKey("DeveloperProfile")]
        [Display(Name = "Developer")]
        public int DeveloperProfileId { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [EmailAddress(ErrorMessage = "{0} is een verkeerde formaat, voorbeeld: voor.beeld@windesheim.nl")]
        [Display(Name = "E-mailadres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Titel")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "{0} moet minimaal {2} kort en {1} lang zijn.")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Bericht is verplicht")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "{0} mag maximaal {1} lang zijn.")]
        [Display(Name = "Bericht")]
        public string? Message { get; set; }

    }
}
