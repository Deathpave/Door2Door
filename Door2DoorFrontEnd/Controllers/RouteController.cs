﻿using Door2DoorFrontEnd.Models;
using Door2DoorLib.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Door2DoorFrontEnd.Controllers
{
    public class RouteController : Controller
    {
        private readonly ILogger<RouteController> _logger;
        private readonly IRouteManager _routeManager;

        public RouteController(ILogger<RouteController> logger, IRouteManager routeManager)
        {
            _logger = logger;
            _routeManager = routeManager;
        }

        [HttpGet("~/navigation")]
        public IActionResult Route(int routeid)
        {
            if (routeid > -1)
            {
                Door2DoorLib.DataModels.Route route = _routeManager.GetRouteById(routeid).Result;
                RouteModel model = new RouteModel();
                model.RouteList = new List<Door2DoorLib.DataModels.Route> { route };
                return View("Route", model);
            }
            return View();
        }
    }
}