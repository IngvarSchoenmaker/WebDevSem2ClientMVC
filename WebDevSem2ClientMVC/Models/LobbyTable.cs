using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDevSem2ClientMVC.Models
{
    public class LobbyTable
    {
        [Key]
        public int TableId { get; set; }

        [Required(ErrorMessage = "Tafelnaam is verplicht")]
        [StringLength(50, MinimumLength =   3, ErrorMessage = "Tafelnaam mag minimaal {2} en maximaal {1} tekens bevatten")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Alleen letters en cijfers toegestaan")]
        public string TableName { get; set; }

        [Required(ErrorMessage = "Aantal spelers is verplicht")]
        [Range(2, 4, ErrorMessage = "Aantal spelers moet tussen 2 en 4 liggen")]
        public int NumberOfPlayers { get; set; }
        [ForeignKey("UnoId")]
        public UnoGame? Game { get; set; }
    }

}
