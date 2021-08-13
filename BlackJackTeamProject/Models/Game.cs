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
        public bool HasGameFinished { get; set; }
        public bool HasStartedGame { get; set; }

        public int NumberPlayers { get; set; }

        public List<Player> Winners = new List<Player>();
        public List<Player> GameWinners = new List<Player>();
        public bool DealerWins { get; set; }

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
            Dealer.HandScore = 0;
            Dealer.Hand = new List<Card>();
        }

        public void StartGame()
        {
            CurrentRound++;
            HasRoundFinished = false;
            HasGameFinished = false;
            ResetPlayers();
            CurrentPlayerIndex = 0;
            System.Console.WriteLine("Player count: " + Players.Count);
            CurrentPlayer = Players[CurrentPlayerIndex]; // Set player 1 as active player

            Deck.Shuffle(); // Shuffle the deck

            Deal(); // Deal cards
            HasStartedGame = true;
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

            // Reset round
            CurrentPlayer = null; // No active player
            HasRoundFinished = true;

            // If no rounds left, end game
            if (CurrentRound == TotalRounds)
            {
                GetGameWinners();
                string test = "Game has ended! Winners are: ";
                if (GameWinners.Count == 0) test += "Dealer";
                foreach (Player player in GameWinners)
                {
                    test += player.Id + ", ";
                }
                System.Console.WriteLine(test);
                HasGameFinished = true;
            }
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
            }
        }

        public List<Player> GetGameWinners()
        {
            float topScore = Dealer.TotalScore;
            List<Player> playersToBeatDealer = new List<Player>();

            foreach (Player player in Players)
            {
                if (player.TotalScore > topScore) playersToBeatDealer.Add(player);
            }

            if (Dealer.TotalScore > 0 && (playersToBeatDealer.Count == 0 || (playersToBeatDealer.Count > 0 && playersToBeatDealer[0].TotalScore == Dealer.TotalScore)))
            {
                DealerWins = true;
            }

            GameWinners = playersToBeatDealer;
            return playersToBeatDealer;
        }

        public List<Player> GetRoundWinners()
        {
            List<Player> playersWhoBeatDealer = new List<Player>();
            bool isTieWithDealer = false;

            foreach (Player player in Players)
            {
                // If not busted, give points
                if (player.HandScore > 0)
                {
                    // If player is winner
                    if (player.HandScore > Dealer.HandScore)
                    {
                        playersWhoBeatDealer.Add(player);
                        player.TotalScore += (float)1;
                    }

                    // Tie
                    else if (player.HandScore == Dealer.HandScore)
                    {
                        player.TotalScore += (float)0.5;
                        isTieWithDealer = true;
                    }
                }
            }

            // If dealer did not bust, give points
            if (Dealer.HandScore > 0)
            {
                // Handle dealer winning/tying
                if (isTieWithDealer) Dealer.TotalScore += (float)0.5;
                else if (playersWhoBeatDealer.Count == 0) Dealer.TotalScore += (float)1;
            }

            return playersWhoBeatDealer;
        }
    }
}