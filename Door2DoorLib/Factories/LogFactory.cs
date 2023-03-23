using Door2DoorLib.Interfaces;
using Door2DoorLib.Logs;

namespace Door2DoorLib.Factories
{
    /// <summary>
    /// Factory that handles the creation of Log objects
    /// </summary>
    public static class LogFactory
    {
        #region Fields
        private static string _errorLogLocation;
        #endregion

        #region Initialize
        /// <summary>
        /// Sets needed data for factory
        /// </summary>
        /// <param name="errorLogLocation"></param>
        public static void Initialize(string errorLogLocation)
        {
            _errorLogLocation = errorLogLocation;
        }
        #endregion

        #region Create Log
        /// <summary>
        /// Returns a log depending on log type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>
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
