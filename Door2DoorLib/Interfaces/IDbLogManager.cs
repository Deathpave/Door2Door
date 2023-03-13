using Door2DoorLib.Logs;

namespace Door2DoorLib.Interfaces
{
    public interface IDbLogManager
    {
        Task<bool> CreateAsync(DatabaseLog createEntity);
    }
}
