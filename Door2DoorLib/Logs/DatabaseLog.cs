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
        private readonly DateTime _timeStamp;

        public string Message { get { return _message; } }
        public MessageTypes MessageType { get { return _messageType; } }
        public DateTime TimeStamp { get { return _timeStamp; } }
        #endregion

        #region Constructor
        public DatabaseLog(long id, MessageTypes messageType, string message, DateTime timeStamp) : base(id)
        {
            _message = message;
            _messageType = messageType;
            _timeStamp = timeStamp;
        }
        #endregion

        public void WriteLog()
        {
            throw new NotImplementedException();
        }
    }
}
