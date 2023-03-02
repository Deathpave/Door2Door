using Door2DoorLib.Interfaces;

namespace Door2DoorLib.Repositories
{
    internal class AdminRepository : IAdminRepository
    {
        #region Fields
        private IDatabase _database;
        #endregion

        #region Constructor
        public AdminRepository(IDatabase database)
        {
            _database = database;
        }
        #endregion
    }
}
