using System.Collections.Generic;
using System.Linq;
using System;

namespace BlackJackTeamProject.Models
{
    public static class Deck
    {
        private static List<string> ranks = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

        public static List<Card> deck = new List<Card>();

        public static void BuildDeck()
        {
            BuildUp("Spades");
            BuildUp("Diamonds");
            BuildUp("Hearts");
            BuildUp("Clubs");
        }

        private static void BuildUp(string suit)
        {
            ranks.ForEach(
                item =>
                {
                    Deck.deck.Add(new Card(suit, item));
                });
        }

        public static void EmptyDeck()
        {

            Deck.deck = new List<Card>();
        }

        private static List<Card> ShuffleDeck()
        {
            Random rnd = new Random();
            deck = Deck.deck.Select(x => new { value = x, order = rnd.Next() })
            .OrderBy(x => x.order).Select(x => x.value).ToList();
            return deck;
            //  Thank you..Cœur from Stackoverflow
            //https://stackoverflow.com/questions/273313/randomize-a-listt
        }

        public static void Shuffle()
        {
            EmptyDeck();
            BuildDeck();
            ShuffleDeck();
        }
        public static Card DealCard()
        {
            Card card = Deck.deck[0];
            Deck.deck.RemoveAt(0);
            return card;
        }

    }
}