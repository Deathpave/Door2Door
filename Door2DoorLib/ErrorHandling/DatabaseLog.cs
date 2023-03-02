using Door2DoorLib.Interfaces;
using MySql.Data.MySqlClient;

namespace Door2DoorLib.ErrorHandling
{
    internal class DatabaseLog : ILog
    {
        #region Fields
        private string _error;
        private IDatabase _database;
        #endregion

        #region Constructor
        public DatabaseLog(string error, IDatabase database)
        {
            _error = error;
            _database = database;
        }
        #endregion

        #region Writelog
        public void WriteLog()
        {
            string query = $"{_error}";
            MySqlCommand mySqlCommand = new MySqlCommand(query);
            _database.ExecuteCommandAsync(mySqlCommand);
        }
        #endregion
    }
}
