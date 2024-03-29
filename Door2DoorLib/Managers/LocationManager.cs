﻿using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Repositories;

namespace Door2DoorLib.Managers
{
    /// <summary>
    /// Manager class for LocationManager entity data handling
    /// </summary>
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
        /// <summary>
        /// Adds a new location entity to the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Location location, Admin admin)
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
        /// <summary>
        /// Deletes a location entity from the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Location location, Admin admin)
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
        /// <summary>
        /// Returns all Location entities from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                LogFactory.CreateLog(LogTypes.Database, ex.Message, MessageTypes.Error).WriteLog();
                return await Task.FromResult(new List<Location>());
            }
        }
        #endregion

        #region Get By Id
        /// <summary>
        /// Returns a Location entity matching the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Location> GetByIdAsync(long id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Location failed = null;
                LogFactory.CreateLog(LogTypes.Database, ex.Message, MessageTypes.Error).WriteLog();
                return await Task.FromResult(failed);
            }
        }
        #endregion

        #region Update Location
        /// <summary>
        /// Updates a Location entity in the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Location location, Admin admin)
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
