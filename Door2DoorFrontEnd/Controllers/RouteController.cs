﻿using Door2DoorFrontEnd.Models;
using Door2DoorLib.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Door2DoorFrontEnd.Controllers
{
    public class RouteController : Controller
    {
        private readonly ILogger<RouteController> _logger;
        private readonly IRouteManager _routeManager;

        // Constructor
        public RouteController(ILogger<RouteController> logger, IRouteManager routeManager)
        {
            _logger = logger;
            _routeManager = routeManager;
        }

        // Navigation page
        [HttpGet("~/navigation")]
        public IActionResult Route(int startid, int endid)
        {
            if (startid > -1 && endid > -1)
            {
                Door2DoorLib.DataModels.Route route = _routeManager.GetByLocationsAsync(startid, endid).Result;
                RouteModel model = new RouteModel();
                model.RouteList = new List<Door2DoorLib.DataModels.Route> { route };
                return View("Route", model);
            }
            return View();
        }


    }
}
