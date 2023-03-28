using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Door2DoorLibTester.Setup
{
    internal class SqlConfigurationSetup
    {
        public static IDatabase SetupDB()
        {
            //var inMemorySettings = new Dictionary<string, string?>
            //{
            //    {
            //        "ConnectionStrings:DefaultConnection",
            //        "Server=10.13.0.125;" +
            //        "Database=door2doordb;" +
            //        "Uid=root;Pwd=123;"
            //    },
            //};

            var inMemorySettings = new Dictionary<string, string?>
            {
                {
                    "ConnectionStrings:DefaultConnection",
                    "Server=(localdb)\\MSSQLLOCALDB;" +
                    "Database=Door2DoorDB;"
                },
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            //var db = DatabaseFactory.CreateDatabase(config, "door2doordb", DatabaseTypes.MySql);
            var db = DatabaseFactory.CreateDatabase(config, "Door2DoorDB", DatabaseTypes.MsSql);

            return db;
        }
    }
}
