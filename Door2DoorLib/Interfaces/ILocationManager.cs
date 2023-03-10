using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    public interface ILocationManager
    {
        Task<bool> AddLocationAsync(Location location, Admin admin);
        Task<bool> DeleteLocationAsync(Location location, Admin admin);
        Task<bool> UpdateLocationAsync(Location location, Admin admin);
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<Location> GetLocationByIdAsync(int id);
    }
}
