using System.Collections.Generic;
using System.Linq;
using System;

namespace BlackJackTeamProject.Models
{
    public class Deck
    {
        private int id = 1;
        private List<string> ranks = new List<string>(){"Ace","2","3","4","5","6","7","8","9","10","Jack","Queen","King"};
        private List<Card> NewDeck = new List<Card>();

        public List<Card> Shuffled = new List<Card>();

        public Deck()
        {
            BuildUp("Spades","black");
            BuildUp("Diamonds","red");
            BuildUp("Hearts","red");
            BuildUp("Clubs","black");
        }

        private void BuildUp(string suit, string color)
        {
            ranks.ForEach(
                item => {
                    NewDeck.Add(new Card(suit, color, item, id));
                    id++;
                });
        }

        public List<Card> Shuffle()
        {
            Random rnd = new Random();
            Shuffled = NewDeck.Select(x => new { value = x, order = rnd.Next() })
            .OrderBy(x => x.order).Select(x => x.value).ToList();
            return Shuffled;
            //  Thank you..Cœur from Stackoverflow
            //https://stackoverflow.com/questions/273313/randomize-a-listt
        }
    }
}