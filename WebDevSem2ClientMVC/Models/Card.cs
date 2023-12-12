namespace WebDevSem2ClientMVC.Models
{
    public class Card
    {
        public int Number { get; set; } // Het nummer op de kaart (0-9)
        public CardColor Color { get; set; } // De kleur van de kaart
        public CardType Type { get; set; } // Het type kaart (bijv. normaal, omkeren, overslaan, etc.)

        // Andere eigenschappen die specifiek zijn voor Uno-kaarten kunnen hier worden toegevoegd

        public override string ToString()
        {
            if (Type == CardType.Number)
            {
                // Een eenvoudige weergave van de kaart, bijvoorbeeld "R2" voor een rode 2
                return $"{Color.ToString()[0]}{Number}";
            }
            return Type.ToString(); // Bijvoorbeeld, "Wild" of "WildDrawFour"
        }
    }

    public enum CardColor
        {
            Red,
            Green,
            Blue,
            Yellow,
            Wild
        }

    public enum CardType
    {
        Number,
        Skip,
        Reverse,
        DrawTwo,
        Wild,
        WildDrawFour
    }
}
