using Door2DoorLib.ErrorHandling;
using Door2DoorLib.Interfaces;

namespace Door2DoorLib.Factories
{
    public static class LogFactory
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
        public static ILog CreateLog(LogTypes type, string messsage, MessageType messageType)
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
                    log = new ConsoleLog(messsage);
                    break;
            }
            return log;
        }
        #endregion
    }
}
