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
            try
            {
                model = SetLists(model);
                Admin admin = new Admin(model.NewAdminUsername, model.NewAdminPswd);
                Admin currentadmin = new Admin(model.Username, "");
                await _adminManager.CreateAsync(admin, currentadmin);
                model.NewAdminUsername = "";
                model.NewAdminPswd = "";
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("Admin", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(AdminModel model)
        {
            try
            {
                model = SetLists(model);
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
                model = SetLists(model);
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
                model.Username = "TestUser";
                model = SetLists(model);
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
                model = SetLists(model);
                Door2DoorLib.DataModels.Route route = new Door2DoorLib.DataModels.Route("", "", 0, 0, model.DeleteRoute);
                Admin admin = new Admin(model.Username, "");
                await _routeManager.DeleteAsync(route, admin);
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("Admin");
            }
        }

        private AdminModel SetLists(AdminModel model)
        {
            if (model.LocationList == null || model.LocationList.Count() == 0)
            {
                model.LocationList = _locationManager.GetAllAsync().Result.ToList();
            }
            if (model.RouteList == null || model.RouteList.Count() == 0)
            {
                model.RouteList = _routeManager.GetAllAsync().Result.ToList();
            }
            return model;
        }
    }
}
