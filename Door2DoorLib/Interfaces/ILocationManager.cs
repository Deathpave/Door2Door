using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    public interface ILocationManager
    {
        /// <summary>
        /// Adds a new location entity to the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(Location location, Admin admin);

        /// <summary>
        /// Deletes a location entity from the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Location location, Admin admin);

        /// <summary>
        /// Updates a Location entity in the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Location location, Admin admin);

        /// <summary>
        /// Returns all Location entities from the database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Location>> GetAllAsync();

        /// <summary>
        /// Returns a Location entity matching the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Location> GetByIdAsync(int id);
    }
}
