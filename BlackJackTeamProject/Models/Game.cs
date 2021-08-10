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

        public static Dealer Dealer = new Dealer();

        public bool HasRoundFinished { get; set; }

        // Deals out cards & sets active player
        public void StartGame()
        {
            HasRoundFinished = false;
            CurrentPlayerIndex = 0;
            System.Console.WriteLine("Player count: " + Players.Count);
            CurrentPlayer = Players[CurrentPlayerIndex]; // Set player 1 as active
            Dealer = new Dealer();
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
            System.Console.WriteLine("Player " + CurrentPlayer.Name + " got a " + newCard.Value);
            System.Console.WriteLine("Player " + CurrentPlayer.Name + " now has " + CurrentPlayer.Hand.Count + " cards");
            System.Console.WriteLine("Player " + CurrentPlayer.Name + " now has a score of " + CurrentPlayer.RoundScore);
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
            if (HasRoundFinished) return;

            Card newCard = Deck.DealCard(); // Get new card
            Dealer.Hand.Add(newCard); // Add card to player's hand
            Console.WriteLine("Dealer got a " + newCard.Value);
            Console.WriteLine("Dealer now has " + Dealer.Hand.Count + " cards");
            Console.WriteLine("Dealer now has a score of " + Dealer.RoundScore);
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
                System.Console.WriteLine("Dealer score is 22+");
                Bust(true);
                EndGame();
                return;
            }

            if (Dealer.RoundScore >= 17)
            {
                System.Console.WriteLine("Dealer score is 17+");
                EndGame();
                return;
            }
        }

        public void Bust(bool isDealer = false)
        {
            if (!isDealer)
            {
                CurrentPlayer.RoundScore = 0; // Reset player's score
                ChangePlayerTurn();
            }
            else
            {
                Dealer.RoundScore = 0; // Reset dealer's score
            }
        }

        public void EndGame()
        {
            // TODO: Check for rounds remaining to see if new round can be made, otherwise determine overall winner
            // TODO: (frontend) Show final results and a start new round button which calls StartGame()
            GetRoundWinners();
            CurrentPlayer = null; // No active player
            HasRoundFinished = true;
        }

        // Changes play to next player's turn
        public void ChangePlayerTurn()
        {
            CurrentPlayerIndex += 1; // Increment index

            // If all players have played (when game is over)
            if (CurrentPlayerIndex >= Players.Count)
            {
                CurrentPlayer = null;
                DealerHit();
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