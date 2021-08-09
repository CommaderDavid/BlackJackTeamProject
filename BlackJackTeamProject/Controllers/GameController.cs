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
            return new {
                hand = Game.CurrentPlayer.Hand,
                index = Game.game.CurrentPlayerIndex
            };
        }

        [HttpPost]
        [Route("/start")]
        public void StartGame()
        {
            Game.game.StartGame();
        }

        [HttpPost]
        [Route("/hit/{name}")]
        public void hit(string name)
        {

        }

        [HttpPost]
        [Route("/hold/")]
        public void hold()
        {

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
