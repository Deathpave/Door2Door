using Door2DoorFrontEnd.Models;
using Door2DoorLib.DataModels;
using Door2DoorLib.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            model.LocationList = _locationManager.GetAllAsync().Result.ToList();
            model.RouteList = _routeManager.GetAllAsync().Result.ToList();
            List<SelectListItem> routes = new List<SelectListItem>();
            SelectListItem t = new SelectListItem();
            foreach (var item in model.RouteList)
            {
                routes.Add(new SelectListItem() { Value = item.Id.ToString(), Text = model.LocationList.Where(x => x.Id == item.StartLocation).FirstOrDefault().Name + "-" + model.LocationList.Where(x => x.Id == item.EndLocation).FirstOrDefault().Name });
                //routes.Add(new string[] { item.Id.ToString(), model.LocationList.Where(x => x.Id == item.StartLocation).FirstOrDefault().Name, model.LocationList.Where(x => x.Id == item.EndLocation).FirstOrDefault().Name });
            }
            model.RouteLocationList = routes;

            return View("Admin", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminModel model)
        {
            AdminModel newmodel = new AdminModel();
            try
            {
                Admin admin = new Admin(model.Username,model.NewAdminPswd);
                Admin currentadmin = new Admin(model.Username,"");
                await _adminManager.CreateAsync(admin,currentadmin);
                return View("Admin", newmodel);
            }
            catch (Exception)
            {
                return View("Admin", newmodel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(AdminModel model)
        {
            try
            {
                Admin admin = new Admin(model.Username, "");
                Admin deleteadmin = new Admin(model.DeleteAdmin, model.Username);

                await _adminManager.DeleteAsync(deleteadmin, admin);
                return View("Admin", model);
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
                Location location = new Location(model.NewLocationName, "");
                Admin currentadmin = new Admin(model.Username, null);

                _locationManager.CreateAsync(location, currentadmin);
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("Admin", model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddRoute(AdminModel model)
        {
            try
            {
                string url = _routeManager.UploadVideoAsync(model.Video).Result;
                Door2DoorLib.DataModels.Route newroute = new Door2DoorLib.DataModels.Route(url, model.NewRouteDescription, model.SelectedStartLocation, model.SelectedEndLocation, 66);
                Admin admin = new Admin(model.Username, "");
                _routeManager.CreateAsync(newroute, admin);
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("Admin");
            }
        }

        [HttpPost]
        public async Task<ActionResult> RemoveRoute(AdminModel model)
        {
            try
            {
                Door2DoorLib.DataModels.Route route = new Door2DoorLib.DataModels.Route("","",0,0,model.DeleteRoute);
                Admin admin = new Admin(model.Username, "");
                await _routeManager.DeleteAsync(route,admin);
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("Admin");
            }
        }
    }
}
