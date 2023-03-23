using Door2DoorLib.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Door2DoorFrontEnd.Models
{
    // Model to handle location data
    public class LocationModel
    {
        public List<SelectListItem> SelectLocationList { get; set; }
        public List<Location> LocationList { get; set; }

        public int StartId { get; set; }

        public int EndId { get; set; }
    }
}
