﻿using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Repositories;

namespace Door2DoorLib.Managers
{
    public class RouteManager : IRouteManager
    {
        #region Fields
        private RouteRepository _repository;
        #endregion

        #region Constructor
        public RouteManager(IDatabase database)
        {
            _repository = new RouteRepository(database);
        }
        #endregion

        #region Methods
        #region Add Route Async
        /// <summary>
        /// Adds a new route to the database
        /// </summary>
        /// <param name="route"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public async Task<bool> AddRouteAsync(Route route, Admin admin)
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
        public async Task<bool> DeleteRouteAsync(Route route, Admin admin)
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
        public async Task<IEnumerable<Route>> GetAllRoutesAsync()
        {
            return await _repository.GetAllAsync();
        }
        #endregion

        #region Get Route By Id
        /// <summary>
        /// Get route by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Route> GetRouteByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        #endregion

        #region Get Route By Location Ids
        /// <summary>
        /// Get route by location ids
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <returns></returns>
        public async Task<Route> GetRouteByLocationIdsAsync(int startLocation, int endLocation)
        {
            return await _repository.GetByLocations(startLocation, endLocation);
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
        public async Task<bool> UpdateRouteAsync(Route route, Admin admin)
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
