using Door2DoorLib.Factories;
using Microsoft.Extensions.Configuration;

namespace Door2DoorLibTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            IConfiguration config = new ConfigurationBuilder().Build();
            var db = DatabaseFactory.CreateDatabase(config, "localdb", Door2DoorLib.Factories.DatabaseTypes.MySql);
            Door2DoorLib.Managers.RouteManager routeManager = new Door2DoorLib.Managers.RouteManager(db);
            ErrorLogFactory.Initialize("", db);
            ErrorLogFactory.CreateLog(Door2DoorLib.Factories.LogTypes.Database, "Something horrible went wrong").WriteLog();
        }
    }
}