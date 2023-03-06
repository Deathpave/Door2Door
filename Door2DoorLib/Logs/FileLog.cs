using Door2DoorLib.Interfaces;

namespace Door2DoorLib.Logs
{
    internal class FileLog : ILog
    {
        #region Fields
        private string _logLocation;
        private string _message;
        #endregion

        #region Constructor
        public FileLog(string message, string logLocation)
        {
            _logLocation = logLocation;
            _message = message;
        }
        #endregion

        #region Write Log
        public void WriteLog()
        {
            File.AppendAllText(_logLocation, _message + "\n");
        }
        #endregion
    }
}
