using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;

namespace Door2DoorLib.Logs
{
    public class DatabaseLog : BaseEntity, ILog
    {
        #region Fields
        private readonly string _message;
        private readonly MessageTypes _messageType;
        private readonly DateTime _date;

        public string Message { get { return _message; } }
        public MessageTypes MessageType { get { return _messageType; } }
        public DateTime TimeStamp { get { return _date; } }
        #endregion

        #region Constructor
        public DatabaseLog(long id, MessageTypes messageType, string message, DateTime date) : base(id)
        {
            _message = message;
            _messageType = messageType;
            _date = date;
        }
        #endregion

        public void WriteLog()
        {
            
        }
    }
}
