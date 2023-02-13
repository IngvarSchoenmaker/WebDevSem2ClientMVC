namespace WebDevSem2ClientMVC.Models
{
    public class DeveloperProfile
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
        public string Discription { get; set; }
        public string PictureURL { get; set; }
        public string Email { get; set; }

        public DeveloperProfile(string name, List<string> skills, string discription, string pictureURL, string email)
        {
            Id = 1;
            Name = name;
            Skills = skills;
            Discription = discription;
            PictureURL = pictureURL;
            Email = email;
        }
    }
}
