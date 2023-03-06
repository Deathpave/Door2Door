using Door2DoorLib.Interfaces;
using MySql.Data.MySqlClient;

namespace Door2DoorLib.Logs
{
    internal class DatabaseLog : ILog
    {
        #region Fields
        private string _message;
        private IDatabase _database;
        #endregion

        #region Constructor
        public DatabaseLog(string message, DateTime date, IDatabase database)
        {
            _message = message;
            _database = database;
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
