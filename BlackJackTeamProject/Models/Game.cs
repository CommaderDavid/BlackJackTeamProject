using System.Collections.Generic;
using System.Linq;

namespace BlackJackTeamProject.Models
{
	public class Game
	{
		public static List<Player> Players { get; set; }
		public static Player CurrentPlayer { get; set; }

		public void StartGame()
		{
			Players[0].State = Player.PlayerState.ActiveTurn; // Set player 1 as active

			// TODO: Randomize deck
			// TODO: Deal cards
			// TODO: Set player turn (for mvp just set dealer or player)
		}

		public void Hold()
		{
			CurrentPlayer.State = Player.PlayerState.InactiveTurn; // End player's turn
		}

		public void Hit()
		{
			CurrentPlayer.Hand.Add(Deck.DealACard());

			// TODO: Update player's score
			// TODO: Check player score for > 21
			// TODO: Player's turn continues if < 21
		}

		public void Bust()
		{
			CurrentPlayer.State = Player.PlayerState.Lost; // End player's turn
			CurrentPlayer.RoundScore = 0; // Reset player's score
		}

		public void EndGame()
		{
			// TODO: End all turns (if not already)
			// TODO: Show final results
			// TODO: Allow starting new game
		}

		// Returns the active player
		public Player GetCurrentPlayer()
		{
			CurrentPlayer = Players.First(player => player.State == Player.PlayerState.ActiveTurn);
			return CurrentPlayer;
		}

		public void ChangePlayerTurn()
		{
			// TODO: Update current player
		}
	}
}