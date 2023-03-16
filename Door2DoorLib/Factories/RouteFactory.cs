using Door2DoorLib.DataModels;

namespace Door2DoorLib.Factories
{
    public class RouteFactory
    {
        #region Methods
        #region Create Route
        public static Route CreateRoute(string videoUrl, string description, long startLocationId, long endLocationId, long id = 0)
        {
            return new Route(videoUrl, description, startLocationId, endLocationId, id);
        }
        #endregion
        #endregion
    }
}
