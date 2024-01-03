using BowlingScoreInterface.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Numerics;
using System.Text.Json;

namespace BowlingScoreInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method to return the view of the home page.
        /// </summary>
        /// <returns>The main view.</returns>
        public IActionResult Index()
        {
            HomeModel model=new();
            return View(model);
        }

        /// <summary>
        /// Method to add a player to the list of players
        /// </summary>
        /// <param name="home">The actual model.</param>
        /// <param name="name"> The name that we want to add.</param>
        /// <returns>The main view with the updated Model.</returns>
        [HttpPost]
        public IActionResult AddPlayer(string home,string name)
        {
            HomeModel model;
            try
            {
                model = JsonSerializer.Deserialize<HomeModel>(home) ?? new() ;
            }
            catch (JsonException e)
            {
                model = new();
            }
            model.Players.Add(name);
            return View(nameof(Index),model);
        }

        /// <summary>
        /// Method to edit the name of a player.
        /// </summary>
        /// <param name="home">The actual model.</param>
        /// <param name="idRename">The index of the name that we want to edit.</param>
        /// <param name="newName">The new name.</param>
        /// <returns>The main view with the updated Model.</returns>
        [HttpPost]
        public IActionResult EditPlayer(string home,int idRename, string newName)
        {
            HomeModel model;
            try
            {
                model = JsonSerializer.Deserialize<HomeModel>(home) ?? new();
            }
            catch (JsonException e)
            {
                model = new();
            }
            model.Players[idRename] = newName;
            return View(nameof(Index), model);
        }

        /// <summary>
        /// Method to delete a player from the list of players.
        /// </summary>
        /// <param name="home">The actual model.</param>
        /// <param name="idPlayer">The index of the player that we want to remove.</param>
        /// <returns>The main view with the updated Model.</returns>
        public IActionResult DeletePlayer(string home,int idPlayer)
        {
            HomeModel model;
            try
            {
                model = JsonSerializer.Deserialize<HomeModel>(home) ?? new();
            }
            catch (JsonException e)
            {
                model = new();
            }
            model.Players.RemoveAt(idPlayer);
            return View(nameof(Index), model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
