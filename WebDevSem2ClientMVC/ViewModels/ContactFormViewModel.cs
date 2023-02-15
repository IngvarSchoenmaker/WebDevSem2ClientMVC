using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDevSem2ClientMVC.ViewModels
{
    public class ContactFormViewModel
    {
        [NotMapped]
        public string? Token { get; set; }
    }
}
