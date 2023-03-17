using Door2DoorFrontEnd.Models;
using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySqlX.XDevAPI;

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
        public IActionResult Admin()
        {
            AdminModel model = new AdminModel();
            model = SetLists(model);

            return View("Admin", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Auth model)
        {
            if (!ModelState.IsValid)
            {
                if (model !=null)
                {
                    //set session
                }
            }
            return View("Admin", model);
        }


        [HttpGet("/admin/menu")]
        public IActionResult AdminMenu()
        {
            AdminModel model = new AdminModel();
            //model.LocationList = _locationManager.GetAllAsync().Result.ToList();
            //model.RouteList = _routeManager.GetAllAsync().Result.ToList();
            //List<SelectListItem> routes = new List<SelectListItem>();
            //SelectListItem t = new SelectListItem();
            //foreach (var item in model.RouteList)
            //{
            //    routes.Add(new SelectListItem() { Value = item.Id.ToString(), Text = model.LocationList.Where(x => x.Id == item.StartLocation).FirstOrDefault().Name + "-" + model.LocationList.Where(x => x.Id == item.EndLocation).FirstOrDefault().Name });
            //    //routes.Add(new string[] { item.Id.ToString(), model.LocationList.Where(x => x.Id == item.StartLocation).FirstOrDefault().Name, model.LocationList.Where(x => x.Id == item.EndLocation).FirstOrDefault().Name });
            //}
            //model.RouteLocationList = routes;
            AdminModel model = new AdminModel();
            //model.LocationList = _locationManager.GetAllAsync().Result.ToList();
            //model.RouteList = _routeManager.GetAllAsync().Result.ToList();
            //List<SelectListItem> routes = new List<SelectListItem>();
            //SelectListItem t = new SelectListItem();
            //foreach (var item in model.RouteList)
            //{
            //    routes.Add(new SelectListItem() { Value = item.Id.ToString(), Text = model.LocationList.Where(x => x.Id == item.StartLocation).FirstOrDefault().Name + "-" + model.LocationList.Where(x => x.Id == item.EndLocation).FirstOrDefault().Name });
            //    //routes.Add(new string[] { item.Id.ToString(), model.LocationList.Where(x => x.Id == item.StartLocation).FirstOrDefault().Name, model.LocationList.Where(x => x.Id == item.EndLocation).FirstOrDefault().Name });
            //}
            //model.RouteLocationList = routes;
            model = SetLists(model);

            return View("Admin", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminModel model)
        {
            try
            {
                model = SetLists(model);
                Admin newAdmin = AdminFactory.CreateAdmin(model.NewAdminUsername, model.NewAdminPswd);
                Admin currentAdmin = AdminFactory.CreateAdmin(model.Username);
                await _adminManager.CreateAsync(newAdmin, currentAdmin);
                model.NewAdminUsername = "";
                model.NewAdminPswd = "";
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("AdminMenu", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(AdminModel model)
        {
            try
            {
                model = SetLists(model);
                Admin admin = AdminFactory.CreateAdmin(model.Username);
                Admin deleteAdmin = AdminFactory.CreateAdmin(model.DeleteAdmin, model.Username);

                await _adminManager.DeleteAsync(deleteAdmin, admin);
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("AdminMenu", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddLocation(AdminModel model)
        {
            try
            {
                model = SetLists(model);
                Location location = LocationFactory.CreateLocation(model.NewLocationName);
                Admin currentadmin = AdminFactory.CreateAdmin(model.Username);

                _locationManager.CreateAsync(location, currentadmin);
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("AdminMenu", model);
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
                Door2DoorLib.DataModels.Route newroute = RouteFactory.CreateRoute(url, model.NewRouteDescription, model.SelectedStartLocation, model.SelectedEndLocation, 66);
                Admin admin = AdminFactory.CreateAdmin(model.Username);
                _routeManager.CreateAsync(newroute, admin);
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("AdminMenu");
            }
        }

        [HttpPost]
        public async Task<ActionResult> RemoveRoute(AdminModel model)
        {
            try
            {
                model = SetLists(model);
                Door2DoorLib.DataModels.Route route = RouteFactory.CreateRoute("", "", 0, 0, model.DeleteRoute);
                Admin admin = AdminFactory.CreateAdmin(model.Username);
                await _routeManager.DeleteAsync(route, admin);
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("AdminMenu");
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
            if (model.RouteLocationList == null || model.RouteLocationList.Count() == 0)
            {
                List<SelectListItem> routeLocationList = new List<SelectListItem>();
                SelectListItem t = new SelectListItem();
                foreach (var item in model.RouteList)
                {
                    routeLocationList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = model.LocationList.Where(x => x.Id == item.StartLocation).FirstOrDefault().Name + "-" + model.LocationList.Where(x => x.Id == item.EndLocation).FirstOrDefault().Name });
                }
                model.RouteLocationList = routeLocationList;
            }
            return model;
        }
    }
}
