using System.Collections.Generic;

namespace BlackJackTeamProject.Models
{
	public class Player
	{
		public enum PlayerState
		{
			ActiveTurn,
			InactiveTurn,
			Lost
		}

		public string Name { get; set; }
		public float RoundScore { get; set; }
		public PlayerState State { get; set; }
		public List<Card> Hand { get; set; }

		public Player(string name)
		{
			Name = name;
			RoundScore = 0;
			State = PlayerState.InactiveTurn;
		}
	}
}