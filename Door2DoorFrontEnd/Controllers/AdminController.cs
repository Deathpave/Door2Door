using Door2DoorFrontEnd.Models;
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
        private readonly ILocationManager _locationManager;
        public AdminController(ILogger<AdminController> logger, IAdminManager adminmanager, IRouteManager routemanager, ILocationManager locationmanager)
        {
            _logger = logger;
            _adminManager = adminmanager;
            _routeManager = routemanager;
            _locationManager = locationmanager;
        }
        [HttpGet("/admin")]
        public IActionResult AdminMenu()
        {
            //Door2DoorLib.DataModels.Admin admin = _adminManager.
            AdminModel model = new AdminModel();


            return View("Admin",model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminModel model)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                //    //add new admin to database here
                //}
                await _routeManager.UploadVideoAsync(model.Video.Name,model.Video.ContentType);
                return RedirectToAction("Admin", model);
            }
            catch (Exception)
            {
                return View("Admin", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddLocation(AdminModel model)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                //    //add new admin to database here
                //}
                Location location = new Location(0,model.NewLocationName,"");
                Admin currentadmin = new Admin(0,model.Username,null);
                _locationManager.CreateAsync(location,currentadmin);
                return RedirectToAction("Admin", model);
            }
            catch (Exception)
            {
                return View("Admin", model);
            }
        }

        [HttpPost]
        public ActionResult AddRoute(AdminModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Location startlocation = new Location(0,model.NewRouteStart,"");
                    //Location endlocation = new Location(0, model.NewRouteEnd, "");
                    
                    //Door2DoorLib.DataModels.Route newroute = new Door2DoorLib.DataModels.Route(0,model.NewRouteVideoUrl,model.NewRouteDescription, startlocation,endlocation);
                    //Admin admin = new Admin(-1, model.Username,"");

                    //_routeManager.CreateAsync(model.newroute,admin);
                }
                return View("Admin",model);
            }
            catch (Exception)
            {
                return View("Admin");
            }
        }
    }
}
