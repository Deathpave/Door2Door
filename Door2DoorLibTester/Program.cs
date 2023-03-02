using Door2DoorLib;
using Microsoft.Extensions.Configuration;

namespace Door2DoorLibTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            IConfiguration config = new ConfigurationBuilder().Build();
            var db = Door2DoorLib.Factories.DatabaseFactory.CreateMySqlDatabase(config, "localdb");
            Door2DoorLib.Managers.RouteManager routeManager = new Door2DoorLib.Managers.RouteManager(db);
        }
    }
}