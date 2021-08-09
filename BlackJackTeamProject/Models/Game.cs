using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackTeamProject.Models
{
    public class Game
    {
        public static Game game = new Game();
        public static List<Player> Players = new List<Player>();
        public static Player CurrentPlayer { get; set; }
        public int CurrentPlayerIndex = 0;

        public static Dealer Dealer { get; set; }

        // Deals out cards & sets active player
        public void StartGame()
        {
            CurrentPlayerIndex = 0;
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

			// CPU's automated turn
			else if (CurrentPlayer.IsCPU)
			{
				Random rand = new Random();

				// Handle difficulties
				switch (CurrentPlayer.cpuDifficulty)
				{
					// Easy difficulty
					case Player.CpuDifficulty.Easy:

						if (CurrentPlayer.RoundScore >= 17)
						{
							// Pick random # between 0 and 10
							if (rand.Next(0, 11) < 5) Hit();
							else Hold();
						}

						break;
				}
			}
        }

        public void DealerHit()
        {
            Card newCard = Deck.DealCard(); // Get new card
            Dealer.Hand.Add(newCard); // Add card to player's hand
            if (newCard.Rank == "Ace")
            {
                if (Dealer.RoundScore + 11 <= 21)
                {
                    Dealer.RoundScore += 11; // Update player's score
                }
                else
                {
                    Dealer.RoundScore += 1; // Update player's score
                }
            }
            else
            {
                Dealer.RoundScore += newCard.Value; // Update player's score
            }


            // Check for bust
            if (Dealer.RoundScore > 21)
            {
                Bust(true);
            }

            if (Dealer.RoundScore >= 17)
            {
                EndGame();
            }

            DealerHit();
        }

        public void Bust(bool isDealer = false)
        {
            if (!isDealer)
            {
                CurrentPlayer.RoundScore = 0; // Reset player's score
                ChangePlayerTurn();
            }
        }

        public void EndGame()
        {
            // TODO: Check for rounds remaining to see if new round can be made, otherwise determine overall winner
            // TODO: (frontend) Show final results and a start new round button which calls StartGame()
            GetRoundWinners();
            CurrentPlayer = null; // No active player
        }

        // Changes play to next player's turn
        public void ChangePlayerTurn()
        {
            CurrentPlayerIndex += 1; // Increment index

            // If all players have played (when game is over)
            if (CurrentPlayerIndex >= Players.Count)
            {
                CurrentPlayer = null;
            }
            else
            {
                CurrentPlayer = Players[CurrentPlayerIndex]; // Set current player

				// If CPU, take CPU's turn
				if (CurrentPlayer.IsCPU)
				{
					Hit();
				}
            }
        }

        public List<Player> GetRoundWinners()
        {
            Dictionary<Player, float> allPlayerScores = new Dictionary<Player, float>();
            List<Player> playersWithTopScore = new List<Player>();
            float topScore;

            // Iterates through each player to get all scores
            for (int i = 0; i < Game.Players.Count; i++)
            {
                Player player = Game.Players[i];
                allPlayerScores.Add(player, player.RoundScore);
                player.TotalScore += player.RoundScore;
            }

            // Sets the top score
            topScore = allPlayerScores.Values.Max();

            // Iterates through each score to determine which players scored the highest
            foreach (KeyValuePair<Player, float> kvp in allPlayerScores)
            {
                if (kvp.Value >= topScore) playersWithTopScore.Add(kvp.Key);
            }

            return playersWithTopScore;
        }
    }
}