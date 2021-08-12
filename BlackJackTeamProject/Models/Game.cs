using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackTeamProject.Models
{
    public class Game
    {
        public int Id { get; set; }
        public static List<Game> games = new List<Game>();
        public List<Player> Players = new List<Player>();
        public Player CurrentPlayer { get; set; }
        public int CurrentPlayerIndex = 0;

        public Dealer Dealer = new Dealer();

        public int CurrentRound = 0;
        public int TotalRounds { get; set; }
        public bool HasRoundFinished { get; set; }

        public int NumberPlayers { get; set; }

        // public int TotalBet { get; set; }

        // Deals out cards & sets active player

        public Game(int id, int totalRounds)
        {
            Id = id;
            TotalRounds = totalRounds;
        }

        public void ResetPlayers()
        {
            Players.ForEach(x => x.Hand = new List<Card>());
            Players.ForEach(x => x.HandScore = 0);
        }

        public void StartGame()
        {
            CurrentRound++;
            HasRoundFinished = false;
            ResetPlayers();
            CurrentPlayerIndex = 0;
            System.Console.WriteLine("Player count: " + Players.Count);
            CurrentPlayer = Players[CurrentPlayerIndex]; // Set player 1 as active

            // Set up bet
            // for (int i = 0; i < Players.Count; i++)
            // {
            //     TotalBet += Players[i].BetAmount;
            // }

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
                    player.HandScore += newCard.Value; // Update player's score
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
            CurrentPlayer.HandScore += newCard.Value; // Update player's score
            System.Console.WriteLine("Player " + CurrentPlayer.Id + " got a " + newCard.Value);
            System.Console.WriteLine("Player " + CurrentPlayer.Id + " now has " + CurrentPlayer.Hand.Count + " cards");
            System.Console.WriteLine("Player " + CurrentPlayer.Id + " now has a score of " + CurrentPlayer.HandScore);

            // Check for bust
            if (CurrentPlayer.HandScore > 21)
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

                        if (CurrentPlayer.HandScore >= 17)
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
           // if (HasRoundFinished) return;

            Card newCard = Deck.DealCard(); // Get new card
            Dealer.Hand.Add(newCard); // Add card to player's hand
            Console.WriteLine("Dealer got a " + newCard.Value);
            Console.WriteLine("Dealer now has " + Dealer.Hand.Count + " cards");
            if (newCard.Rank == "Ace")
            {
                if (Dealer.HandScore + 11 <= 21)
                {
                    Dealer.HandScore += 11; // Update player's score
                }
                else
                {
                    Dealer.HandScore += 1; // Update player's score
                }
            }
            else
            {
                Dealer.HandScore += newCard.Value; // Update player's score
            }

            Console.WriteLine("Dealer now has a score of " + Dealer.HandScore);


            // Check for bust
            if (Dealer.HandScore > 21)
            {
                System.Console.WriteLine("Dealer score is 22+");
                Bust(true);
                EndGame();
                return;
            }

            if (Dealer.HandScore >= 17)
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
                CurrentPlayer.HandScore = 0; // Reset player's score
                ChangePlayerTurn();
            }
            else
            {
                Dealer.HandScore = 0; // Reset dealer's score
            }
        }

        public void EndGame()
        {
            // Hand out winnings for round
            List<Player> winners = GetRoundWinners();

            // If not tie
            // if (winners.Count == 1) winners[0].TotalWinnings += TotalBet;

            // Reset round
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
            //int totalBet = 0;

            // Iterates through each player to get all scores
            for (int i = 0; i < Players.Count; i++)
            {
                Player player = Players[i];
                allPlayerScores.Add(player, player.HandScore);
                //totalBet += player.BetAmount;
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