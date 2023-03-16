using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Logs;
using Door2DoorLib.Repositories;

namespace Door2DoorLib.Managers
{
    public class DbLogManager : IDbLogManager
    {
        #region Fields
        private readonly DbLogRepository _repository;
        #endregion

        #region Constructor
        public DbLogManager(IDatabase database)
        {
            _repository = new DbLogRepository(database);
        }
        #endregion

        #region Methods
        #region Create Async
        public async Task<bool> CreateAsync(DatabaseLog createEntity)
        {
            if (_repository.CreateAsync(createEntity).Result)
            {
                return await Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.File, createEntity.Message, createEntity.MessageType).WriteLog();
                return await Task.FromResult(false);
            }
        }
        #endregion
        #endregion
    }
}
