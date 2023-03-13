﻿using Door2DoorFrontEnd.Models;
using Door2DoorLib.DataModels;
using Door2DoorLib.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Door2DoorFrontEnd.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminManager _adminManager;
        private readonly IRouteManager _routeManager;
        public AdminController(ILogger<AdminController> logger, IAdminManager adminmanager, IRouteManager routemanager)
        {
            _logger = logger;
            _adminManager = adminmanager;
            _routeManager = routemanager;
        }
        [HttpGet("/admin")]
        public IActionResult AdminMenu()
        {
            //Door2DoorLib.DataModels.Admin admin = _adminManager.
            AdminModel model = new AdminModel();
            return View("Admin",model);
        }

        [HttpPost]
        public ActionResult AddAdmin(AdminModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AdminModel adminModel = new AdminModel();
                    adminModel.newadmin = model.newadmin;
                    //add new admin to database here
                }
                return View("Admin");
            }
            catch (Exception)
            {
                return View("Admin");
            }
        }

        [HttpPost]
        public ActionResult AddRoute(AdminModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RouteModel routeModel = new RouteModel();
                    Admin admin = new Admin(-1, model.Username,"");
                    _routeManager.CreateAsync(model.newroute,admin);
                }
                return View("Admin");
            }
            catch (Exception)
            {
                return View("Admin");
            }
        }
    }
}