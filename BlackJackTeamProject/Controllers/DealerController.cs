using System;
using System.Collections.Generic;
using BlackJackTeamProject.Models;
using Microsoft.AspNetCore.Mvc;
//using .net 3.1 

namespace BlackJackTeamProject.Controllers
{
  [Produces("application/json")]
  [Route("/dealer")]
  public class DealerController : Controller
  {

    [Route("/dealer/card")]
    [HttpGet]
    public Card GetCard()
    {
      if (Deck.deck.Count == 0)
      {
        Deck.EmptyNewDeckAndDeck();
        Deck.BuildDeck();
        Deck.ShuffleDeck();
      }

      return Deck.DealACard();
    }

    [Route("/dealer/empty")]
    [HttpGet]
    public void Empty()
    {
      Deck.EmptyNewDeckAndDeck();
    }



  }
}