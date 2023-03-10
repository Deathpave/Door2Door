using Door2DoorLib.DataModels;
namespace Door2DoorFrontEnd.Models
{
    public class LocationModel
    {
        public List<Location> LocationList { get; set; } = new List<Location>();

        public int StartId { get; set; }

        public int EndId { get; set; }
    }
}
