using Door2DoorLib.Factories;
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
            _sqlConnection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion

        #region Methods
        #region Open Connection Async
        // Opens the database connection
        public override Task<bool> OpenConnectionAsync()
        {
            try
            {
                _sqlConnection.Open();
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                LogFactory.CreateLog(LogTypes.File, $"Failed to open database connection due to {e.Message}", MessageTypes.Error);
                return Task.FromResult(false);
            }

        }
        #endregion

        #region Close Connection
        // Closes the database connection
        public override void CloseConnection()
        {
            try
            {
                _sqlConnection.Close();
            }
            catch (Exception e)
            {
                LogFactory.CreateLog(LogTypes.File, $"Failed to close database connection due to {e.Message}", MessageTypes.Error);
            }
        }
        #endregion

        #region Execute Command Async
        // Executes sql command
        public override Task<MySqlDataReader?> ExecuteCommandAsync(MySqlCommand sqlCommand)
        {
            try
            {
                sqlCommand.Connection = _sqlConnection;
                return Task.FromResult(sqlCommand.ExecuteReader());
            }
            catch (Exception e)
            {
                LogFactory.CreateLog(LogTypes.File, $"Failed to execute sql command due to {e.Message}", MessageTypes.Error);
                return Task.FromResult<MySqlDataReader?>(null);
            }
        }
        #endregion
        #endregion
    }
}
