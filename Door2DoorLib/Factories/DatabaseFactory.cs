using Door2DoorLib.Adapters;
using Door2DoorLib.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace Door2DoorLib.Factories
{
    public class DatabaseFactory
    {
        public static IDatabase CreateMySqlDatabase(IConfiguration configuration, string databaseName)
        {
            return new MySqlDatabase(configuration, databaseName);
        }
    }
}
