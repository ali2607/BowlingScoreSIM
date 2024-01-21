using BowlingScoreInterface.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BowlingScoreInterface.Controllers
{
    public class GameController : Controller
    {

        /// <summary>
        /// Method to return the view of the game page.
        /// </summary>
        /// <returns>The main view.</returns>
        public IActionResult Index(string serializedGame)
        {
            Game? game = new();
            try
            {
                game = JsonSerializer.Deserialize<Game>(serializedGame);
            }
            catch (JsonException e)
            {
                throw new JsonException("Error while deserializing the game", e);
            }
            return View(game);
        }

        public IActionResult PinsTaken(string serializedGame, int pinsScore)
        {
            Game? game = new();
            try
            {
                game = JsonSerializer.Deserialize<Game>(serializedGame);
            }
            catch (JsonException e)
            {
                throw new JsonException("Error while deserializing the game", e);
            }

            //Mettre a jour le scoreboard
            //Mettre a jour le joueur actuel
            game = game.Update(pinsScore);

            if ((game.Players[^1].BonusRoll == SpecialRoll.Default && game.CurrentRound == game.NumberOfRounds - 1 && game.Players[^1].Rounds[game.NumberOfRounds-2].RoundScore != String.Empty) ||((game.Players[^1].BonusRoll != SpecialRoll.Default && game.CurrentRound == game.NumberOfRounds)))
            {

                string[] playerNames = new string[game.Players.Count];
                int[] playerScores = new int[game.Players.Count];

                for (int i = 0; i < game.Players.Count; i++)
                {
                    playerNames[i] = game.Players[i].Name;
                    playerScores[i] = game.Players[i].TotalScore;
                }

                for (int i = 0; i < playerScores.Length; i++)
                {
                    for (int j = i + 1; j < playerScores.Length; j++)
                    {
                        if (playerScores[i] < playerScores[j])
                        {
                            // Échanger les scores
                            int tempScore = playerScores[i];
                            playerScores[i] = playerScores[j];
                            playerScores[j] = tempScore;

                            // Échanger les noms correspondants
                            string tempName = playerNames[i];
                            playerNames[i] = playerNames[j];
                            playerNames[j] = tempName;
                        }
                    }
                }
                return RedirectToAction(nameof(Index), nameof(Leaderboard), new { names = playerNames, scores = playerScores });
            }

            return View(nameof(Index), game);
        }
    }
}
