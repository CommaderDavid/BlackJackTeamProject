using System.Collections.Generic;

namespace BlackJackTeamProject.Models
{
    public class Game
    {
        public static Game game = new Game();
        public static List<Player> Players { get; set; }
        public static Player CurrentPlayer { get; set; }
        public int CurrentPlayerIndex = 0;

        public static Dealer Dealer { get; set; }

        // Deals out cards & sets active player
        public void StartGame()
        {
            CurrentPlayer = Players[CurrentPlayerIndex]; // Set player 1 as active
            Deck.Shuffle(); // Shuffle the deck
            Deal(); // Deal cards
        }

        // Deal 2 cards to each player
        public void Deal()
        {
            foreach (Player player in Players)
            {
                for (int i = 0; i < 2; i++)
                {
					Card newCard = Deck.DealCard(); // Get new card
                    player.Hand.Add(newCard); // Add card to player's hand
					player.RoundScore += newCard.Value; // Update player's score
                }
            }
        }

        public void Hold()
        {
            CurrentPlayer.GetAceScore();
            ChangePlayerTurn();
        }

        public void Hit()
        {
            Card newCard = Deck.DealCard(); // Get new card
            CurrentPlayer.Hand.Add(newCard); // Add card to player's hand
            CurrentPlayer.RoundScore += newCard.Value; // Update player's score

            // Check for bust
            if (CurrentPlayer.RoundScore > 21)
            {
                Bust();
            }
        }

        public void Bust()
        {
            CurrentPlayer.RoundScore = 0; // Reset player's score
            ChangePlayerTurn();
        }

        public void EndGame()
        {
            // TODO: End all turns (if not already)
            // TODO: Show final results
            // TODO: Allow starting new game
        }

        // Changes play to next player's turn
        public void ChangePlayerTurn()
        {
            CurrentPlayerIndex += 1; // Increment index
            CurrentPlayer = Players[CurrentPlayerIndex]; // Set current player
        }
    }
}