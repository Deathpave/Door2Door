using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    internal interface IRouteRepository : IRepository<Route>
    {
        /// <summary>
        /// Get a route entity with matching location ids
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <returns></returns>
        Task<Route> GetRouteByLocations(long startLocation, long endLocation);
    }
}
