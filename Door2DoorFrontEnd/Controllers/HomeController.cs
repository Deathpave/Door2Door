﻿using Door2DoorFrontEnd.Models;
using Door2DoorLib.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Door2DoorLib.DataModels;

namespace Door2DoorFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IEnumerable<Location> _locations = new List<Location>();
        private readonly ILocationManager _locationManager;

        // Constructor
        public HomeController(ILogger<HomeController> logger, ILocationManager locationManager)
        {
            _logger = logger;
            _locationManager = locationManager;
        }

        // Start page
        [HttpGet("~/")]
        public IActionResult Index(int startid)
        {
            ViewData["locations"] = _locations;

            LocationModel model = new LocationModel();
            model.LocationList = _locationManager.GetAllAsync().Result.ToList();
            model.StartId = startid;
            return View(model);
        }

        // Error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}