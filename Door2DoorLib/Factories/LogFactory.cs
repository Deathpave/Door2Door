using Door2DoorLib.Interfaces;
using Door2DoorLib.Logs;

namespace Door2DoorLib.Factories
{
    public static class LogFactory
    {
        #region Fields
        private static string _errorLogLocation;
        #endregion

        #region Initialize
        // Sets needed data for factory
        public static void Initialize(string errorLogLocation)
        {
            _errorLogLocation = errorLogLocation;
        }
        #endregion

        #region Create Log
        // Returns a log depending on log type
        public static ILog CreateLog(LogTypes type, string message, MessageTypes messageType)
        {
            ILog log = null;
            switch (type)
            {
                case LogTypes.Database:
                    log = new DatabaseLog(0, messageType, message, DateTime.Now);
                    break;
                case LogTypes.File:
                    log = new FileLog(message, DateTime.Now, messageType, _errorLogLocation);
                    break;
                case LogTypes.Console:
                    log = new ConsoleLog(message, DateTime.Now, messageType);
                    break;
            }
            return log;
        }
        #endregion
    }
}
