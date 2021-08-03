using System;
using System.Collections.Generic;
using BlackJackTeamProject.Models;
using Microsoft.AspNetCore.Mvc;
//using .net 3.1 

namespace BlackJackTeamProject.Controllers
{
  public class DealerController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }


    [HttpGet]
    public List<Card> ShuffleAndReturnDeck()
    {
      Deck.EmptyNewDeckAndDeck();
      Deck.BuildDeck();
      return Deck.ShuffleDeck();
    }

    [HttpGet]
    public List<Card> GetDeck()
    {
      if (Deck.deck.Count == 0)
      {
        return Deck.ShuffleDeck();
      }
      else
        return Deck.deck;
    }


    [HttpGet]
    public List<Card> DeckDeal()
    {
      if (Deck.deck.Count == 0)
      {
        return Deck.ShuffleDeck();
      }
      else
        return Deck.deck;
    }
  }
}