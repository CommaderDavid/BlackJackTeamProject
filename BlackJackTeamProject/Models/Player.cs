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
		// TODO: Player hand

		public Player(string name)
		{
			Name = name;
			RoundScore = 0;
			State = PlayerState.InactiveTurn;
		}
	}
}