namespace BlackJackTeamProject.Models
{
  public class Card
  {
    public string Suit { get; set; }
    public string Color { get; set; }
    public string Rank { get; set; }
    public int Value { get; set; }
    
    public Card(string suit, string color, string rank)
    {
      Suit = suit;
      Color = color;
      Rank = rank;

      switch (rank)
      {
        case "Queen":
          Value = 10;
          break;
        case "King":
          Value = 10;
          break;
        case "Jack":
          Value = 10;
          break;
        case "Ace":
          Value = 1;
          break;
        default:
          Value = int.Parse(rank);
          break;
      }
    }
  }
}