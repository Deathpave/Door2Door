using Door2DoorLib.Adapters;
using Door2DoorLib.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Door2DoorLib.Factories
{
    public class DatabaseFactory
    {
        public static IDatabase CreateDatabase(IConfiguration configuration, string databaseName, DatabaseEnums databaseType)
        {
            IDatabase database = null;
            switch (databaseType)
            {
                case DatabaseEnums.MySql:
                    database = new MySqlDatabase(configuration, databaseName);
                    break;
            }
            return database;
        }
    }
}
