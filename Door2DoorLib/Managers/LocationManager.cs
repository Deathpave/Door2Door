using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Repositories;

namespace Door2DoorLib.Managers
{
    public class LocationManager : ILocationManager
    {
        #region Fields
        private readonly LocationRepository _repository;
        #endregion

        #region Constructor
        public LocationManager(IDatabase database)
        {
            _repository = new LocationRepository(database);
        }
        #endregion

        #region Methods

        #region Add Location
        public async Task<bool> AddLocationAsync(Location location, Admin admin)
        {
            if (_repository.CreateAsync(location).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} created location {location.Name}", MessageTypes.Added).WriteLog();
                return await Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to create location {location.Name}", MessageTypes.Error).WriteLog();
                return await Task.FromResult(false);
            }
        }
        #endregion

        #region Delete Location
        public async Task<bool> DeleteLocationAsync(Location location, Admin admin)
        {
            if (_repository.DeleteAsync(location).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} deleted location {location.Name}", MessageTypes.Deleted).WriteLog();
                return await Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed deleted location {location.Name}", MessageTypes.Error).WriteLog();
                return await Task.FromResult(false);
            }
        }
        #endregion

        #region Get all
        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _repository.GetAllAsync();
        }
        #endregion

        #region Get By Id
        public async Task<Location> GetLocationByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        #endregion

        #region Update Location
        public async Task<bool> UpdateLocationAsync(Location location, Admin admin)
        {
            if (_repository.UpdateAsync(location).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} created location {location.Name}", MessageTypes.Added);
                return await Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to create location {location.Name}", MessageTypes.Error);
                return await Task.FromResult(false);
            }
        }
        #endregion

        #endregion
    }
}
