using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Door2DoorLib.Adapters
{
    internal class MySqlDatabase : Database
    {
        #region Fields
        private MySqlConnection _sqlConnection;
        #endregion

        #region Constructor
        public MySqlDatabase(IConfiguration configuration, string databaseName) : base(configuration, databaseName)
        {
            // Creating our database connection
            _sqlConnection = new MySqlConnection(configuration.GetConnectionString(databaseName));
        }
        #endregion

        #region Methods
        #region Open Connection Async
        // Opens the database connection
        public override Task<bool> OpenConnectionAsync()
        {
            _sqlConnection.Open();
            if (_sqlConnection.State == System.Data.ConnectionState.Open)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        #endregion

        #region Close Connection
        // Closes the database connection
        public override void CloseConnection()
        {
            _sqlConnection.Close();
        }
        #endregion

        #region Execute Command Async
        // Executes sql command
        public override Task<MySqlDataReader> ExecuteCommandAsync(MySqlCommand sqlCommand)
        {
            sqlCommand.Connection = _sqlConnection;
            return Task.FromResult(sqlCommand.ExecuteReader());
        }
        #endregion
        #endregion
    }
}
