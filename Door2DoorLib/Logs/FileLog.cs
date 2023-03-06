using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;

namespace Door2DoorLib.Logs
{
    internal class FileLog : ILog
    {
        #region Fields
        private string _logLocation;
        private string _message;
        private DateTime _date;
        private MessageTypes _messageType;
        #endregion

        #region Constructor
        public FileLog(string message, DateTime date, MessageTypes messageType, string logLocation)
        {
            _logLocation = logLocation;
            _message = message;
            _date = date;
            _messageType = messageType;
        }
        #endregion

        #region Write Log
        public void WriteLog()
        {
            File.AppendAllText(_logLocation, $"{_date.ToString("dd-MM-yyyy hh:mm")} - {_messageType.ToString()} - {_message}\n");
        }
        #endregion
    }
}
