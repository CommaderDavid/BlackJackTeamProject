using System.Collections.Generic;
using System.Linq;
namespace BlackJackTeamProject.Models
{
    public class Dealer
    {
        public string Name = "Dealer";
        public int HandScore { get; set; }
        public List<Card> Hand = new List<Card>();
        public float TotalScore { get; set; }

        public Dealer()
        {
            HandScore = 0;
        }

        public void AlterAceValueToGet17Plus()
        {
            List<Card> aces = Hand.Where(x => x.Rank == "Ace").ToList();

            foreach (Card ace in aces)
            {
                if (HandScore + 10 >= 17 && HandScore + 10 <= 21)
                {
                    HandScore += 10;
                }
            }
        }
    }
}