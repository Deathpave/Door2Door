using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;

namespace Door2DoorLib.ErrorHandling
{
    internal class FileLog : ILog
    {
        #region Fields
        private string _logLocation;
        private string _message;
        private MessageType _messageType;
        #endregion

        #region Constructor
        public FileLog(string message, DateTime date, MessageType messageType, string logLocation)
        {
            _logLocation = logLocation;
            _message = message;
            _messageType = messageType;
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
