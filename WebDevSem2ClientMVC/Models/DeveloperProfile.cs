using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebDevSem2ClientMVC.Models
{
    public class DeveloperProfile
    {
        [Key]
        [Required]
        public int DeveloperProfileId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Skills { get; set; }
        [Required]
        public string? Discription { get; set; }
        [Required]
        [Url]
        public string? PictureURL { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public DeveloperProfile(string name, string skills, string discription, string pictureURL, string email)
        {
            DeveloperProfileId = 1;
            Name = name;
            Skills = skills;
            Discription = discription;
            PictureURL = pictureURL;
            Email = email;
        }
    }
}
