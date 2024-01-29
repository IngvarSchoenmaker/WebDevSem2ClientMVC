using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDevSem2ClientMVC.Models
{
    public enum GameStatus
    {
        WaitingForPlayers,
        InProgress,
        Ended
    }
    public class UnoGame
    {
        [Key]
        public int UnoId { get; set; }
        public GameStatus GameStatus { get; set; }
        public List<Player> Players { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> DiscardPile { get; set; }
        [ForeignKey("PlayerId")]
        public Player? CurrentPlayer;

        //niet database
        public Player? You;
        public UnoGame() 
        {
            Players = new List<Player>();
            GameStatus = GameStatus.WaitingForPlayers;
            Deck = InitializeDeck();
            ShuffleDeck();
            DiscardPile = new List<Card> { DrawCard() }; // Eerste kaart op de aflegstapel
        }
        public UnoGame(Player players)
        {
            Players = new List<Player>();
            GameStatus = GameStatus.WaitingForPlayers;
            JoinGame(players);
            Deck = InitializeDeck();
            ShuffleDeck();
            DiscardPile = new List<Card> { DrawCard() }; // Eerste kaart op de aflegstapel
            CurrentPlayer = Players.First(); // Start met de eerste speler
        }
        public List<Card> GetStartingHand()
        {
            List<Card> hand = new List<Card>();
            int startingAmount = 7;
            for (int i = 0; i < startingAmount; i++)
            {
                hand.Add(DrawCard());
            }
            return hand;
        }
        public void JoinGame(Player player)
        {
            player.HandCards = GetStartingHand();
            Players.Add(player);
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
            Deck = Deck.OrderBy(card => Guid.NewGuid()).ToList();
        }

        private Card DrawCard()
        {
            var card = Deck.First();
            Deck.Remove(card);
            return card;
        }

        public Card PlayCard(string playerId, Card playedCard)
        {
            // Controleer of de kaart geldig is om te spelen
            if (IsValidMove(playedCard))
            {
                // Voeg de gespeelde kaart toe aan de aflegstapel
                DiscardPile.Add(playedCard);

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

            var topCard = DiscardPile.Last();

            return playedCard.Color == topCard.Color || playedCard.Number == topCard.Number || playedCard.Type == CardType.Wild;
        }

        private void ChangeCurrentPlayer()
        {
            // Implementeer de logica om de huidige speler te wijzigen, bijv. naar de volgende speler in de lijst
            // Dit is een eenvoudig voorbeeld; je moet dit aanpassen aan de regels van Uno
            var currentPlayerIndex = Players.IndexOf(CurrentPlayer);
            var nextPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
            CurrentPlayer = Players[nextPlayerIndex];
        }
        public void ChangeGameStatus()
        {
            // Andere logica blijft ongewijzigd

            // Voorbeeld: Zet de status op 'InProgress' als het spel begint
            if (GameStatus == GameStatus.WaitingForPlayers && Players.Count >= 2)
            {
                GameStatus = GameStatus.InProgress;
            }

            // Voorbeeld: Zet de status op 'Ended' als het spel eindigt
            if (GameStatus == GameStatus.InProgress && IsGameOver())
            {
                GameStatus = GameStatus.Ended;
            }
        }
        private bool IsGameOver()
        {
            return Players.Any(player => player.HandCards.Count == 0);
        }
    }

}
