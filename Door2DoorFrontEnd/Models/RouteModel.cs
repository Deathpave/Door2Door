using Door2DoorLib.DataModels;

namespace Door2DoorFrontEnd.Models
{
    public class RouteModel
    {
        public List<Door2DoorLib.DataModels.Route> RouteList { get; set; }
        public LocationModel LocationModel { get; set; }
        public string NewRouteStart { get; set; }
        public string NewRouteEnd { get; set; }
        public string NewRouteDescription { get; set; }
        public string NewRouteVideoUrl { get; set; }
        public long DeleteRoute { get; set; }
    }
}
