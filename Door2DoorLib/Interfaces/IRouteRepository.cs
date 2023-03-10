using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    internal interface IRouteRepository : IRepository<Route>
    {
        Task<Route> GetByLocations(long startLocation, long endLocation);
    }
}
