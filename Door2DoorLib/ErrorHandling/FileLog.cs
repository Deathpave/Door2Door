using Door2DoorLib.Interfaces;

namespace Door2DoorLib.ErrorHandling
{
    internal class FileLog : ILog
    {
        #region Fields
        private string _logLocation;
        private string _error;
        #endregion

        #region Constructor
        public FileLog(string error, string logLocation)
        {
            _logLocation = logLocation;
            _error = error;
        }
        #endregion

        #region Write Log
        public void WriteLog()
        {
            File.AppendAllText(_logLocation, _error + "\n");
        }
        #endregion
    }
}
