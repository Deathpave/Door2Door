using Door2DoorLib.ErrorHandling;
using Door2DoorLib.Interfaces;

namespace Door2DoorLib.Factories
{
    public static class ErrorLogFactory
    {
        #region Fields
        private static string _errorLogLocation;
        private static IDatabase _database;
        #endregion

        #region Initialize
        public static void Initialize(string errorLogLocation, IDatabase database)
        {
            _errorLogLocation = errorLogLocation;
            _database = database;
        }
        #endregion

        #region Create Log
        public static ILog CreateLog(LogTypes type, string error)
        {
            ILog log = null;
            switch (type)
            {
                case LogTypes.Database:
                    log = new DatabaseLog(error, _database);
                    break;
                case LogTypes.File:
                    log = new FileLog(error, _errorLogLocation);
                    break;
                case LogTypes.Console:
                    log = new ConsoleLog(error);
                    break;
            }
            return log;
        }
        #endregion
    }
}
