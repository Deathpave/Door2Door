using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    public interface ILocationManager
    {
        // <summary>
        /// Adds a new location entity to the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        Task<bool> AddLocationAsync(Location location, Admin admin);

        /// <summary>
        /// Deletes a location entity from the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        Task<bool> DeleteLocationAsync(Location location, Admin admin);

        /// <summary>
        /// Updates a Location entity in the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        Task<bool> UpdateLocationAsync(Location location, Admin admin);

        /// <summary>
        /// Returns all Location entities from the database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Location>> GetAllLocationsAsync();

        /// <summary>
        /// Returns a Location entity matching the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Location> GetLocationByIdAsync(int id);
    }
}
