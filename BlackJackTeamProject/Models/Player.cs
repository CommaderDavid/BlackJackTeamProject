using System.Collections.Generic;
using System.Linq;

namespace BlackJackTeamProject.Models
{
    public class Player
    {
        public int Id;
		public enum CpuDifficulty
		{
			Easy,
			Medium,
			Hard,
			Impossible
		}

        public int HandScore { get; set; }
        public List<Card> Hand = new List<Card>();
		public bool IsCPU { get; set; }
		public CpuDifficulty cpuDifficulty { get; set; }
        // public int BetAmount { get; set; }
		// public float TotalWinnings { get; set; }

        public Player(int id)
        {
            Id = id;
            HandScore = 0;
			cpuDifficulty = CpuDifficulty.Easy;
        }

        public void GetAceScore()
        {
            List<Card> aces = Hand.Where(x => x.Rank == "Ace").ToList();

            foreach (Card ace in aces)
            {
                if (HandScore + 10 <= 21)
                {
                    HandScore += 10;
                }
            }
        }
    }
}