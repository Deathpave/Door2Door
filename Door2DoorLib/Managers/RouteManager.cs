using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Repositories;

namespace Door2DoorLib.Managers
{
    public class RouteManager
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
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} created {route.Name}", MessageTypes.Added).WriteLog();
                return Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to create route {route.Name}", MessageTypes.Error).WriteLog();
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
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} deleted route {route.Name}", MessageTypes.Deleted).WriteLog();
                return Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed deleted route {route.Name}", MessageTypes.Error).WriteLog();
                return Task.FromResult(false);
            }
        }
        #endregion
        #endregion
    }
}
