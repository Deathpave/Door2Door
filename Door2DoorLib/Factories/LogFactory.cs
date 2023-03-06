using Door2DoorLib.Interfaces;
using Door2DoorLib.Logs;

namespace Door2DoorLib.Factories
{
    public static class LogFactory
    {
        #region Fields
        private static string _errorLogLocation;
        private static IDatabase _database;
        #endregion

        #region Initialize
        // Sets needed data for factory
        public static void Initialize(string errorLogLocation, IDatabase database)
        {
            _errorLogLocation = errorLogLocation;
            _database = database;
        }
        #endregion

        #region Create Log
        // Returns a log depending on log type
        public static ILog CreateLog(LogTypes type, string messsage, MessageTypes messageType)
        {
            ILog log = null;
            switch (type)
            {
                case LogTypes.Database:
                    log = new DatabaseLog(messsage, DateTime.Now, messageType, _database);
                    break;
                case LogTypes.File:
                    log = new FileLog(messsage, DateTime.Now, messageType, _errorLogLocation);
                    break;
                case LogTypes.Console:
                    log = new ConsoleLog(messsage, DateTime.Now, messageType);
                    break;
            }
            return log;
        }
        #endregion
    }
}
