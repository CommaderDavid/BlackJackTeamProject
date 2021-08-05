using System.Collections.Generic;
using System.Linq;

namespace BlackJackTeamProject.Models
{
  public class Player
  {
    public string Name { get; set; }
    public float RoundScore { get; set; }
    public List<Card> Hand { get; set; }

    public Player(string name)
    {
      Name = name;
      RoundScore = 0;
    }

    public void GetAceScore()
    {
      List<Card> aces = Hand.Where(x => x.Rank == "Ace").ToList();

      foreach (Card ace in aces)
      {
        if (RoundScore + 10 <= 21)
        {
          RoundScore += 10;
        }
      }
    }
  }
}