using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Repositories;
using Microsoft.AspNetCore.Http;

namespace Door2DoorLib.Managers
{
    /// <summary>
    /// Manager class for Route entity data handling
    /// </summary>
    public class RouteManager : IRouteManager
    {
        #region Fields
        private readonly RouteRepository _repository;
        #endregion

        #region Constructor
        public RouteManager(IDatabase database)
        {
            _repository = new RouteRepository(database);
        }
        #endregion

        #region Methods
        #region Upload Video
        /// <summary>
        ///  Uploads video to fpt server, and returns url for video
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            string result = _repository.UploadVideoAsync(file).Result;
            if (result != string.Empty)
            {
                return await Task.FromResult(result);
            }
            else
            {
                return await Task.FromResult(string.Empty);
            }
        }
        #endregion

        #region Add Route Async
        /// <summary>
        /// Adds a new route to the database
        /// </summary>
        /// <param name="route"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Route route, Admin admin)
        {
            if (_repository.CreateAsync(route).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} created {route.StartLocation}-{route.EndLocation}", MessageTypes.Added).WriteLog();
                return await Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to create route {route.StartLocation}-{route.EndLocation}", MessageTypes.Error).WriteLog();
                return await Task.FromResult(false);
            }
        }
        #endregion

        #region Delete Route Async
        /// <summary>
        /// Deletes a route from the database
        /// </summary>
        /// <param name="route"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Route route, Admin admin)
        {
            if (_repository.DeleteAsync(route).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} deleted route {route.StartLocation}-{route.EndLocation}", MessageTypes.Deleted).WriteLog();
                return await Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed deleted route {route.StartLocation}-{route.EndLocation}", MessageTypes.Error).WriteLog();
                return await Task.FromResult(false);
            }
        }
        #endregion

        #region Get Routes
        #region Get All Routes Async
        /// <summary>
        /// Get all routes
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Route>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                LogFactory.CreateLog(LogTypes.Database, ex.Message, MessageTypes.Error).WriteLog();
                return await Task.FromResult(new List<Route>());
            }
        }
        #endregion

        #region Get Route By Id
        /// <summary>
        /// Get route by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Route> GetByIdAsync(long id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Route failed = null;
                LogFactory.CreateLog(LogTypes.Database, ex.Message, MessageTypes.Error).WriteLog();
                return await Task.FromResult(failed);
            }
        }
        #endregion

        #region Get Route By Location Ids
        /// <summary>
        /// Get route by location ids
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <returns></returns>
        public async Task<Route> GetByLocationsAsync(long startLocation, long endLocation)
        {
            try
            {
                return await _repository.GetByLocationsAsync(startLocation, endLocation);
            }
            catch (Exception ex)
            {
                Route failed = null;
                LogFactory.CreateLog(LogTypes.Database, ex.Message, MessageTypes.Error).WriteLog();
                return await Task.FromResult(failed);
            }
        }
        #endregion
        #endregion

        #region Update Route Async
        /// <summary>
        /// Update route
        /// </summary>
        /// <param name="route"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Route route, Admin admin)
        {
            if (_repository.UpdateAsync(route).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} created {route.StartLocation}-{route.EndLocation}", MessageTypes.Added);
                return await Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to create route {route.StartLocation}-{route.EndLocation}", MessageTypes.Error);
                return await Task.FromResult(false);
            }
        }
        #endregion
        #endregion
    }
}
