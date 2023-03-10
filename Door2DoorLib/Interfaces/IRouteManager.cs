using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    public interface IRouteManager
    {
        Task<bool> AddRouteAsync(Route route, Admin admin);
        Task<bool> DeleteRouteAsync(Route route, Admin admin);
        Task<bool> UpdateRouteAsync(Route route, Admin admin);
        Task<IEnumerable<Route>> GetAllRoutesAsync();
        Task<Route> GetRouteByIdAsync(int id);
        Task<Route> GetRouteByLocationIdsAsync(int startLocation, int endLocation);
    }
}
