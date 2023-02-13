using Microsoft.Build.Framework;

namespace WebDevSem2ClientMVC.ViewModels
{
    public class ContactFormViewModel
    {
        [Required]
        public string Token { get; set; }
    }
}
