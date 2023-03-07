using Door2DoorLib.Adapters;
using Door2DoorLib.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Door2DoorLib.Factories
{
    public class DatabaseFactory
    {
        #region Create Database
        // Creates a database instance
        public static IDatabase CreateDatabase(IConfiguration configuration, string databaseName, DatabaseTypes databaseType)
        {
            IDatabase database = null;
            switch (databaseType)
            {
                case DatabaseTypes.MySql:
                    database = new MySqlDatabase(configuration, databaseName);
                    break;
            }
            return database;
        }
        #endregion
    }
}
