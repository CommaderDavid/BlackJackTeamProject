using System.Collections.Generic;

namespace BlackJackTeamProject.Models
{
  public class Dealer
  {
    public string Name = "Dealer";
    public int HandScore{get; set;}
    public List<Card> Hand = new List<Card>();
    public float TotalScore { get; set; }

    public Dealer()
    {
      HandScore = 0;
    }
  }
}