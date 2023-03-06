using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using MySql.Data.MySqlClient;

namespace Door2DoorLib.ErrorHandling
{
    internal class DatabaseLog : ILog
    {
        #region Fields
        private string _message;
        private IDatabase _database;
        private MessageType _messageType;
        #endregion

        #region Constructor
        public DatabaseLog(string message, DateTime date, MessageType messageType, IDatabase database)
        {
            _message = message;
            _database = database;
            _messageType = messageType;
        }
        #endregion

        #region Writelog
        public void WriteLog()
        {
            string query = $"{_message}";
            MySqlCommand mySqlCommand = new MySqlCommand(query);
            _database.ExecuteCommandAsync(mySqlCommand);
        }
        #endregion
    }
}
