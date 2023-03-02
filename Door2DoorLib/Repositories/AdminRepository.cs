using Door2DoorLib.Interfaces;

namespace Door2DoorLib.Repositories
{
    internal class AdminRepository : IAdminRepository
    {
      private IDatabase _database;

        public AdminRepository(IDatabase database)
        {
            _database = database;
        }
    }
}
