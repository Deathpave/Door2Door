using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Repositories;

namespace Door2DoorLib.Managers
{
    public class RouteManager : IRouteManager
    {
        #region Fields
        private RouteRepository _routeRepository;
        #endregion

        #region Constructor
        public RouteManager(IDatabase database)
        {
            _routeRepository = new RouteRepository(database);
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
        public Task<bool> AddRouteAsync(Route route, Admin admin)
        {
            if (_routeRepository.CreateAsync(route).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} created {route.StartLocation}-{route.EndLocation}", MessageTypes.Added).WriteLog();
                return Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to create route {route.StartLocation}-{route.EndLocation}", MessageTypes.Error).WriteLog();
                return Task.FromResult(false);
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
        public Task<bool> DeleteRouteAsync(Route route, Admin admin)
        {
            if (_routeRepository.DeleteAsync(route).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} deleted route {route.StartLocation}-{route.EndLocation}", MessageTypes.Deleted).WriteLog();
                return Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed deleted route {route.StartLocation}-{route.EndLocation}", MessageTypes.Error).WriteLog();
                return Task.FromResult(false);
            }
        }
        #endregion

        #region Update Route Async
        /// <summary>
        /// Update route
        /// </summary>
        /// <param name="route"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public Task<bool> UpdateRouteAsync(Route route, Admin admin)
        {
            if (_routeRepository.UpdateAsync(route).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} created {route.StartLocation}-{route.EndLocation}", MessageTypes.Added);
                return Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to create route {route.StartLocation}-{route.EndLocation}", MessageTypes.Error);
                return Task.FromResult(false);
            }
        }
        #endregion

        #region Get Routes
        #region Get Route Async
        /// <summary>
        /// Get all routes
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Route>> GetAllRoutesAsync()
        {
            return _routeRepository.GetAllAsync();
        }
        #endregion

        #region Get Route By Id
        /// <summary>
        /// Get route by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Route> GetRouteById(int id)
        {
            return _routeRepository.GetByIdAsync(id);
        }
        #endregion

        #region Get Route By Location Ids
        /// <summary>
        /// Get route by location ids
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <returns></returns>
        public Task<Route> GetRouteByLocationIds(int startLocation, int endLocation)
        {
            return _routeRepository.GetByLocations(startLocation, endLocation);
        }
        #endregion
        #endregion
        #endregion
    }
}
