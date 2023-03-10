using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    internal interface IRouteRepository
    {
        public Task<bool> CreateAsync(Route createEntity);
        public Task<bool> DeleteAsync(Route deleteEntity);
        public Task<Route> GetByIdAsync(long id);
        public Task<IEnumerable<Route>> GetAllAsync();
        public Task<bool> UpdateAsync(Route updateEntity);
        public Task<Route> GetByLocations(long startLocation, long endLocation);
    }
}
