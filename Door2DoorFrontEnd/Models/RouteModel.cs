using Door2DoorLib.DataModels;

namespace Door2DoorFrontEnd.Models
{
    public class RouteModel
    {
        public List<Door2DoorLib.DataModels.Route> RouteList  { get; set; }
        public LocationModel StartLocation { get; set; }
        public LocationModel EndLocation { get; set; }
    }
}
