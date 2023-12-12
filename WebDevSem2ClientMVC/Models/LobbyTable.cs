using System.ComponentModel.DataAnnotations;

namespace WebDevSem2ClientMVC.Models
{
    public class LobbyTable
    {
        [Key]
        public int TableId { get; set; }

        [Required(ErrorMessage = "Tafelnaam is verplicht")]
        [StringLength(50, ErrorMessage = "Tafelnaam mag maximaal 50 tekens bevatten")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Alleen letters en cijfers toegestaan")]
        public string TableName { get; set; }

        [Required(ErrorMessage = "Aantal spelers is verplicht")]
        [Range(2, 4, ErrorMessage = "Aantal spelers moet tussen 2 en 4 liggen")]
        public int NumberOfPlayers { get; set; }
    }

}
