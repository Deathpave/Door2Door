using d2dfrontend.Models;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Managers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace d2dfrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IEnumerable<Door2DoorLib.DataModels.Route> _routes = new List<Door2DoorLib.DataModels.Route>();
        private RouteManager _routeManager;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public HomeController(ILogger<HomeController> logger, IDatabase database)
        //{
        //    _logger = logger;
        //    _routeManager = new RouteManager(database);
        //}

        public IActionResult Index()
        {
            //_routes = _routeManager.GetRoutesAsync().Result;

            //ViewData["routes"] = _routes;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Guide()
        {
            //Get Video url from video object tied to route object, based off of the selection from the index dropdown menu

            //GuideViewModel temp = new GuideViewModel(
            //    _routeManager.GetRouteAsync("insert selection from mapoverview page here").Result.VideoUrl
            //    );

            GuideViewModel model = new GuideViewModel("/media/movie.mp4");



            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}