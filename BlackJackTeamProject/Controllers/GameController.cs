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
            int Count = Game.games.Count;
            return View(Count);
        }

        [HttpGet]
        [Route("/getallhands/{id}")]
        public object GetAllHands(int id)
        {
            Game game = Game.games.Find(x => x.Id == id);
            string gameState = "";
            List<float> playerScores = game.Players.Select(x => x.TotalScore).ToList();
            if (game.HasGameFinished == true)
            {
                gameState = "gameover";
            }
            else
            if (game.CurrentPlayer == null && game.HasRoundFinished != true)
            {
                gameState = "dealer";
            }
            else if (game.HasRoundFinished)
            {
                gameState = "roundover";
            }
            List<List<Card>> hands = game.Players.Select(x => x.Hand).ToList();
            hands.Add(game.Dealer.Hand);
          
            return new
            {
                hands = hands,
                gameState = gameState,
                currentPlayer = (game.CurrentPlayer != null) ? game.CurrentPlayer.Id : -1,
                currentRound = game.CurrentRound,
                totalRounds = game.TotalRounds,
                playerScores = playerScores,
                dealerScore = game.Dealer.TotalScore,
                playerWinners = game.GameWinners.Select(x => x.Id).ToList(),
                dealerWon = game.DealerWins
            };
        }

        [HttpPost]
        [Route("/start/{id}")]
        public void StartGame(int id)
        {
            Game game = Game.games.Find(x => x.Id == id);
            if (!game.HasGameFinished && ((game.HasStartedGame && game.HasRoundFinished) || !game.HasStartedGame))
            {
                game.StartGame();
            }
        }

        [HttpPost]
        [Route("/hit/{id}")]
        public void hit(int id)
        {
            Game game = Game.games.Find(x => x.Id == id);
            if (game.CurrentPlayer != null)
            {
                game.Hit();
            }

        }

        [HttpPost]
        [Route("/dealerhit/{id}")]
        public void dealerHit(int id)
        {
            Game game = Game.games.Find(x => x.Id == id);

            game.DealerHit();
        }

        [HttpPost]
        [Route("/hold/{id}")]
        public void hold(int id)
        {
            Game game = Game.games.Find(x => x.Id == id);
            if (game.CurrentPlayer != null)
            {
                game.Hold();
            }
        }

        [HttpPost]
        [Route("/makeplayer/{number}/{id}/{rounds}")]
        public void MakePlayer(int number, int id, int rounds)
        {
            Game.games.RemoveAll(x => x.Id == id);
            Game game = new Game(id, rounds);
            Game.games.Add(game);
            for (int i = 0; i < number; i++)
            {
                Player player = new Player(i);
                game.Players.Add(player);
            }
        }
    }
}
