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

        public IActionResult Index()
        {
            HomeModel model=new();
            return View(model);
        }

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
