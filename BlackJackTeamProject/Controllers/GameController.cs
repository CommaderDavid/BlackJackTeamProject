//create api controller

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BlackJackTeamProject.Models;
namespace BlackJackTeamProject.Controllers
{
    public class GameController : Controller
    {

        [HttpGet]
        [Route("/")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home
        [HttpGet]
        [Route("/showhand/{name}")]
        public List<Card> ShowHandByName(string name)
        {
            if (name == "dealer")
            {

            }
            List<Player> players = Game.Players;
            Player player = players.Find(x => x.Name == name);
            List<Card> cards = player.Hand;
            return cards;
        }

        [Route("/getactivehand")]
        public object GetActiveHand()
        {
            if (Game.CurrentPlayer == null)
            {
                return new
                {
                    hand = Game.Dealer.Hand,
                    index = -1
                };
            }
            else
            {
                return new
                {
                    hand = Game.CurrentPlayer.Hand,
                    index = Game.game.CurrentPlayerIndex
                };
            }
        }

        [HttpPost]
        [Route("/start")]
        public void StartGame()
        {
            Game.game.StartGame();
        }

        [HttpPost]
        [Route("/hit")]
        public void hit()
        {
            Game.game.Hit();
        }

        [HttpPost]
        [Route("/dealerhit")]
        public void dealerHit()
        {
            Game.game.DealerHit();
        }

        [HttpPost]
        [Route("/hold/")]
        public void hold()
        {
            Game.game.ChangePlayerTurn();
        }

        [HttpPost]
        [Route("/makeplayer/{name}")]
        public void MakePlayer(string name)
        {
            System.Console.WriteLine(name);
            Player player = new Player(name);
            Game.Players.Add(player);
        }
    }
}
