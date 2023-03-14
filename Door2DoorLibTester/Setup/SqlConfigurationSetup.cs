using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Door2DoorLibTester.Setup
{
    internal class SqlConfigurationSetup
    {
        public static IDatabase SetupDB()
        {
            var inMemorySettings = new Dictionary<string, string?>
            {
                {
                    "ConnectionStrings:DefaultConnection", 
                    "Server=127.0.0.1;" +
                    "Database=door2doordb;" +
                    "Uid=root;Pwd=123;"
                },
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var db = DatabaseFactory.CreateDatabase(config, "door2doordb", DatabaseTypes.MySql);

            return db;
        }
    }
}
