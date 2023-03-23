using Microsoft.AspNetCore.Mvc.Rendering;

namespace Door2DoorFrontEnd.Models
{
    // Model to handle route related data
    public class RouteModel
    {
        public List<SelectListItem>? SelectRouteList { get; set; }
        public List<Door2DoorLib.DataModels.Route>? RouteList { get; set; }
        public LocationModel LocationModel { get; set; }
        public string? RouteDescription { get; set; }
        public string? VideoUrl { get; set; }
        public long? Id { get; set; }
        public RouteModel()
        {
            LocationModel = new LocationModel();
        }
    }
}
