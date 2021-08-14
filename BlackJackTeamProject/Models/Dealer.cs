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
            bool isThereAnAce = Hand.Any(x => x.Rank == "Ace");

            if (isThereAnAce && (HandScore + 10 >= 17 && HandScore + 10 <= 21))
            {
                HandScore += 10;
            }
        }
    }
}