﻿using Door2DoorFrontEnd.Models;
using Door2DoorLib.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Door2DoorFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IEnumerable<Door2DoorLib.DataModels.Route> _routes = new List<Door2DoorLib.DataModels.Route>();
        private readonly IRouteManager _routeManager;

        public HomeController(ILogger<HomeController> logger, IRouteManager routeManager)
        {
            _logger = logger;
            _routeManager = routeManager;
        }

        [HttpGet("~/")]
        public IActionResult Index()
        {
            _routes = _routeManager.GetAllRoutesAsync().Result;

            ViewData["routes"] = _routes;

            RouteModel model = new RouteModel();
            model.RouteList = _routes.ToList();
            return View(model);
        }

        [HttpGet("~/navigation")]
        public IActionResult Navigation(int routeid)
        {
            if (routeid > -1)
            {
                Door2DoorLib.DataModels.Route route = _routeManager.GetRouteById(routeid).Result;
                RouteModel model = new RouteModel();
                model.RouteList.Add(route);
                return View("Route",model);
            }
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