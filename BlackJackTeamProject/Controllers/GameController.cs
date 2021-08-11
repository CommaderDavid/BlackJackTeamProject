//create api controller

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BlackJackTeamProject.Models;
using System.Linq;
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

        [HttpGet]
        [Route("/getallhands")]
        public object GetAllHands()
        {
            string gameState = "";
            if (Game.CurrentPlayer == null && Game.game.HasRoundFinished != true)
            {
                gameState = "dealer";
            }
            else if (Game.game.HasRoundFinished)
            {
                gameState = "roundover";
                System.Console.WriteLine("Round over");
            }
            List<List<Card>> hands = Game.Players.Select(x => x.Hand).ToList();
            hands.Add(Game.Dealer.Hand);
            return new
            {
                hands = hands,
                gameState = gameState
            };
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
