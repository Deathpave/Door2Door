using Door2DoorLib.DataModels;
using Door2DoorLib.Repositories;

namespace Door2DoorLib.Managers
{
    public class RouteManager
    {
        #region Fields
        private RouteRepository _routeRepository;
        #endregion

        #region Constructor
        public RouteManager()
        {
            _routeRepository = new RouteRepository();
        }
        #endregion

        #region Methods
        #region Add Route Async
        public Task<bool> AddRouteAsync(Route route)
        {
            return Task.FromResult(true);
        }
        #endregion

        #region Delete Route Async
        public Task<bool> DeleteRouteAsync(Route route)
        {
            return Task.FromResult(true);
        }
        #endregion
        #endregion
    }
}
