using System.Collections.Generic;

namespace BlackJackTeamProject.Models
{
  public class Dealer
  {
    public string Name = "Dealer";
    public float RoundScore{get; set;}
    public List<Card> Hand { get; set; }

    public Dealer()
    {
      RoundScore = 0;
    }
  }
}