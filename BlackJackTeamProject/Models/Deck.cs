using System.Collections.Generic;
using System.Linq;
using System;

namespace BlackJackTeamProject.Models
{
  public static class Deck
  {
    private static int id = 1;
    private static List<string> ranks = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
    public static List<Card> NewDeck = new List<Card>();

    public static List<Card> deck;

    public static void BuildDeck()
    {
      BuildUp("Spades", "black");
      BuildUp("Diamonds", "red");
      BuildUp("Hearts", "red");
      BuildUp("Clubs", "black");
    }

    private static void BuildUp(string suit, string color)
    {
      ranks.ForEach(
          item =>
          {
            NewDeck.Add(new Card(suit, color, item, id));
            id++;
          });
    }

    public static void EmptyNewDeckAndDeck()
    {
      deck = new List<Card>();
      NewDeck = new List<Card>();
    }

    public static List<Card> ShuffleDeck()
    {
      Random rnd = new Random();
      deck = NewDeck.Select(x => new { value = x, order = rnd.Next() })
      .OrderBy(x => x.order).Select(x => x.value).ToList();
      return deck;
      //  Thank you..Cœur from Stackoverflow
      //https://stackoverflow.com/questions/273313/randomize-a-listt
    }

    public static Card DealACard()
    {
      Card card = Deck.deck[0];
      Deck.deck.RemoveAt(0);
      return card;
    }

  }
}