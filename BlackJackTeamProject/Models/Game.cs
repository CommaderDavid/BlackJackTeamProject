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
            HasGameFinished = false; ;
            CurrentRound++;
            HasRoundFinished = false;
            ResetPlayers();
            CurrentPlayerIndex = 0;
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

            // Check for bust
            if (CurrentPlayer.HandScore > 21)
            {
                Bust();
            }
        }

        public void DealerHit()
        {
            if (HasRoundFinished) return;
            Card newCard = Deck.DealCard(); // Get new card
            Dealer.Hand.Add(newCard); // Add card to player's hand

            Dealer.HandScore += newCard.Value; // Update player's score


            Dealer.AlterAceValueToGet17Plus();
            if (Dealer.HandScore >= 17 && Dealer.HandScore <= 21)
            {
                EndRound();
                return;
            }
            // Check for bust
            if (Dealer.HandScore > 21)
            {
                Bust(true);
                EndRound();
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

        public void EndRound()
        {
            // Hand out winnings for round
            GetRoundWinners();

            // Reset round
            CurrentPlayer = null; // No active player
            HasRoundFinished = true;

            // If no rounds left, end game
            if (CurrentRound == TotalRounds)
            {
                GetGameWinners();
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
            }
        }

        public void GetGameWinners()
        {
            float dealerScore = Dealer.TotalScore;
            float playerScore = Players.Max(x => x.TotalScore);

            if (dealerScore > playerScore)
            {
                DealerWins = true;
            }
            else if ((dealerScore == playerScore) && dealerScore > 0)
            {
                DealerWins = true;
                GameWinners = Players.Where(x => x.TotalScore == dealerScore).ToList();
            }
            else if (playerScore > 0)
            {
                GameWinners = Players.Where(x => x.TotalScore == playerScore).ToList();
            }
        }

        public void GetRoundWinners()
        {

            float dealerScore = Dealer.HandScore;
            float playerScore = Players.Max(x => x.HandScore);
            List<Player> roundWinners = new List<Player>();

            if (dealerScore > playerScore)
            {
                Dealer.TotalScore += (float)1;
            }
            else if ((dealerScore == playerScore) && dealerScore > 0)
            {
                roundWinners = Players.Where(x => x.HandScore == dealerScore).ToList();
                foreach (Player player in roundWinners)
                {
                    player.TotalScore += (float)0.5;
                }

            }
            else if (playerScore > 0)
            {
                roundWinners = Players.Where(x => x.HandScore == playerScore).ToList();
                if (roundWinners.Count > 1)
                {
                    foreach (Player player in roundWinners)
                    {
                        player.TotalScore += (float)0.5f;
                    }
                }
                else
                {
                    roundWinners[0].TotalScore += (float)1;
                }
            }
        }
    }
}