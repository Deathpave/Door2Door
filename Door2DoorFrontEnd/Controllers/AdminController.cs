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
            model.LocationList = (IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>)_locationManager.GetAllAsync().Result.ToList();

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
                Location location = new Location(model.NewLocationName,"");
                Admin currentadmin = new Admin(model.Username,null);
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
                    Door2DoorLib.DataModels.Route newroute = new Door2DoorLib.DataModels.Route(model.NewRouteVideoUrl, model.NewRouteDescription, model.SelectedStartLocation,model.SelectedEndLocation);
                    Admin admin = new Admin(model.Username, "");

                    _routeManager.CreateAsync(newroute, admin);
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
