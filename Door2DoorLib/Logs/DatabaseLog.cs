using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace Door2DoorLib.Logs
{
    internal class DatabaseLog : BaseEntity, ILog
    {
        #region Fields
        private string _message;
        private IDatabase _database;
        private MessageTypes _messageType;
        #endregion

        #region Constructor
        public DatabaseLog(long id, string message, DateTime date, MessageTypes messageType, IDatabase database) : base(id)
        {
            _message = message;
            _database = database;
            _messageType = messageType;
        }
        #endregion

        #region Writelog
        // Adds a entry to log table
        public void WriteLog()
        {
            DateTime date = DateTime.Now;

            string query = $"INSERT INTO log(type, description, timestamp) VALUES ({(int)_messageType},{_message},{date.ToTimestamp()})";
            MySqlCommand mySqlCommand = new MySqlCommand(query);
            _database.ExecuteCommandAsync(mySqlCommand);
        }
        #endregion
    }
}
