using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    public interface IRouteManager
    {
        /// <summary>
        /// Adds a new route to the database
        /// </summary>
        /// <param name="route"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(Route route, Admin admin);

        /// <summary>
        /// Deletes a route from the database
        /// </summary>
        /// <param name="route"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Route route, Admin admin);

        /// <summary>
        /// Update route
        /// </summary>
        /// <param name="route"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Route route, Admin admin);

        /// <summary>
        /// Get all routes
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Route>> GetAllAsync();

        /// <summary>
        /// Get route by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Route> GetByIdAsync(int id);

        /// <summary>
        /// Get route by location ids
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <returns></returns>
        Task<Route> GetByLocationsAsync(int startLocation, int endLocation);
    }
}
