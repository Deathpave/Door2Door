using Door2DoorFrontEnd.Models;
using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
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
            model.auth = new AuthModel("", 3);
            return View("Admin", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminModel model)
        {
            if (model != null)
            {
                if (await _adminManager.CheckLoginAsync(model.auth.Username, model.auth.Password))
                {
                    model.auth.Authenticated = 1;
                    model.auth.Username = model.auth.Username;
                    model = SetLists(model);
                }
                else
                {
                    model.auth.Authenticated = 0;
                    return View("Admin", model);
                }
            }
            return View("AdminMenu", model);
        }


        [HttpPost("/admin/menu")]
        public IActionResult AdminMenu(AdminModel model, IFormCollection collection)
        {
            model = SetData(model, collection);

            return View("AdminMenu", model);
        }

        [HttpPost("/admin/addadmin")]
        public async Task<IActionResult> AddAdmin(AdminModel model, IFormCollection collection)
        {
            try
            {
                SetData(model, collection);
                Admin newAdmin = AdminFactory.CreateAdmin(model.NewAdminUsername, model.NewAdminPswd);
                Admin currentAdmin = AdminFactory.CreateAdmin(model.Username);
                await _adminManager.CreateAsync(newAdmin, currentAdmin);
                model.NewAdminUsername = "";
                model.NewAdminPswd = "";
                return View("AdminMenu", model);
            }
            catch (Exception)
            {
                return View("AdminMenu", model);
            }
        }

        [HttpPost("/admin/removeadmin")]
        public async Task<IActionResult> RemoveAdmin(AdminModel model, IFormCollection collection)
        {
            try
            {
                model = SetData(model, collection);
                Admin admin = AdminFactory.CreateAdmin(model.Username);
                Admin deleteAdmin = AdminFactory.CreateAdmin(model.DeleteAdmin, model.Username);

                await _adminManager.DeleteAsync(deleteAdmin, admin);
                return View("AdminMenu", model);
            }
            catch (Exception)
            {
                return View("AdminMenu", model);
            }
        }

        [HttpPost("/admin/addlocation")]
        public async Task<IActionResult> AddLocation(AdminModel model, IFormCollection collection)
        {
            try
            {
                model = SetData(model, collection);
                Location location = LocationFactory.CreateLocation(model.NewLocationName);
                Admin currentadmin = AdminFactory.CreateAdmin(model.Username);

                await _locationManager.CreateAsync(location, currentadmin);
                return View("AdminMenu", model);
            }
            catch (Exception)
            {
                //_logger.LogError();
                return View("AdminMenu", model);
            }
        }

        [HttpPost("/admin/addroute")]
        public async Task<ActionResult> AddRoute(AdminModel model, IFormCollection collection)
        {
            try
            {
                model = SetData(model, collection);
                string url = _routeManager.UploadVideoAsync(model.File.Video).Result;
                Door2DoorLib.DataModels.Route newroute = RouteFactory.CreateRoute(url, model.RouteModel.RouteDescription, model.RouteModel.LocationModel.StartId, model.RouteModel.LocationModel.EndId);
                Admin admin = AdminFactory.CreateAdmin(model.Username);
                await _routeManager.CreateAsync(newroute, admin);
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("AdminMenu");
            }
        }

        [HttpPost("/admin/removeroute")]
        public async Task<ActionResult> RemoveRoute(AdminModel model, IFormCollection collection)
        {
            try
            {
                model = SetData(model, collection);
                Door2DoorLib.DataModels.Route route = RouteFactory.CreateRoute("", "", 0, 0, model.RouteModel.Id);
                Admin admin = AdminFactory.CreateAdmin(model.Username);
                await _routeManager.DeleteAsync(route, admin);
                return View("Admin", model);
            }
            catch (Exception)
            {
                return View("AdminMenu");
            }
        }

        private AdminModel SetData(AdminModel model, IFormCollection collection)
        {
            model = SetData(model, collection);
            return model;
        }

        private AdminModel SetLists(AdminModel model)
        {
            if (model.RouteModel.RouteList == null || model.RouteModel.RouteList.Count() == 0)
            {
                model.RouteModel.RouteList = _routeManager.GetAllAsync().Result.ToList();
            }
            if (model.RouteModel.LocationModel.LocationList == null || model.RouteModel.LocationModel.LocationList.Count() == 0)
            {
                model.RouteModel.LocationModel.LocationList = _locationManager.GetAllAsync().Result.ToList();
            }
            if (model.RouteModel.SelectRouteList == null || model.RouteModel.SelectRouteList.Count() == 0)
            {
                List<SelectListItem> routeLocationList = new List<SelectListItem>();
                SelectListItem t = new SelectListItem();
                foreach (var item in model.RouteModel.RouteList)
                {
                    routeLocationList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = model.RouteModel.LocationModel.LocationList.Where(x => x.Id == item.StartLocation).FirstOrDefault().Name + "-" + model.RouteModel.LocationModel.LocationList.Where(x => x.Id == item.EndLocation).FirstOrDefault().Name });
                }
                model.RouteModel.SelectRouteList = routeLocationList;
            }
            return model;
        }

        private AdminModel SetAuth(AdminModel model, IFormCollection collection)
        {
            collection.TryGetValue("Authusr", out StringValues values);
            model.auth = new AuthModel(values, 1);
            return model;
        }
    }
}
