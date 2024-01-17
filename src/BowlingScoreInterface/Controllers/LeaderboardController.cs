using BowlingScoreInterface.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BowlingScoreInterface.Controllers
{
    public class LeaderboardController : Controller
    {
        /// <summary>
        /// Method to return the view of the Leaderboard page.
        /// </summary>
        /// <returns>The main view.</returns>
        public IActionResult Index(string serializedResult)
        {
            return View();
        }

    }
}
