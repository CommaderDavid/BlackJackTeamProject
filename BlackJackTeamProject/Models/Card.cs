namespace BlackJackTeamProject.Models
{
    public class Card
    {
        public string Suit{get;set;}
        public string Color{get; set;}
        public string Rank{get;set;}

        public int Id {get;set;}

        public Card(string suit, string color, string rank, int id)
        {
            Suit = suit;
            Color = color;
            Rank = rank;
            Id = id;
        }
    }
}