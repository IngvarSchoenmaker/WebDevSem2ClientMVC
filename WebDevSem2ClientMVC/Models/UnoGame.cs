namespace WebDevSem2ClientMVC.Models
{
    public class UnoGame
    {
        private List<Player> _players;
        private List<Card> _deck;
        private List<Card> _discardPile;
        private Player _currentPlayer;

        public UnoGame(List<Player> players)
        {
            _players = players;
            _deck = InitializeDeck();
            ShuffleDeck();
            _discardPile = new List<Card> { DrawCard() }; // Eerste kaart op de aflegstapel
            _currentPlayer = _players.First(); // Start met de eerste speler
        }

        private List<Card> InitializeDeck()
        {
            var deck = new List<Card>();

            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                if (color != CardColor.Wild)
                {
                    // Voeg kaarten 0-9, Skip, Reverse, en DrawTwo toe in elke kleur
                    deck.AddRange(Enumerable.Range(0, 10).Select(number => new Card { Number = number, Color = color, Type = CardType.Number }));
                    deck.AddRange(Enumerable.Repeat(new Card { Color = color, Type = CardType.Skip }, 2));
                    deck.AddRange(Enumerable.Repeat(new Card { Color = color, Type = CardType.Reverse }, 2));
                    deck.AddRange(Enumerable.Repeat(new Card { Color = color, Type = CardType.DrawTwo }, 2));
                }

                // Voeg Wild en WildDrawFour toe
                deck.Add(new Card { Type = CardType.Wild });
                deck.Add(new Card { Type = CardType.WildDrawFour });
            }

            return deck;
        }


        private void ShuffleDeck()
        {
            _deck = _deck.OrderBy(card => Guid.NewGuid()).ToList();
        }

        private Card DrawCard()
        {
            var card = _deck.First();
            _deck.Remove(card);
            return card;
        }

        public Card PlayCard(string playerId, Card playedCard)
        {
            // Controleer of de kaart geldig is om te spelen
            if (IsValidMove(playedCard))
            {
                // Voeg de gespeelde kaart toe aan de aflegstapel
                _discardPile.Add(playedCard);

                // Wijzig de huidige speler (bijv. naar de volgende speler in de lijst)
                ChangeCurrentPlayer();

                return playedCard;
            }

            // Ongeldige zet, retourneer null of een speciale kaart om aan te geven dat de zet niet is toegestaan
            return null;
        }

        private bool IsValidMove(Card playedCard)
        {
            // Implementeer de logica om te controleren of de gespeelde kaart een geldige zet is
            // Dit is een eenvoudig voorbeeld; je moet dit aanpassen aan de regels van Uno

            var topCard = _discardPile.Last();

            return playedCard.Color == topCard.Color || playedCard.Number == topCard.Number || playedCard.Type == CardType.Wild;
        }

        private void ChangeCurrentPlayer()
        {
            // Implementeer de logica om de huidige speler te wijzigen, bijv. naar de volgende speler in de lijst
            // Dit is een eenvoudig voorbeeld; je moet dit aanpassen aan de regels van Uno
            var currentPlayerIndex = _players.IndexOf(_currentPlayer);
            var nextPlayerIndex = (currentPlayerIndex + 1) % _players.Count;
            _currentPlayer = _players[nextPlayerIndex];
        }
    }

}
