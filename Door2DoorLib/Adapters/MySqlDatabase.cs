using Door2DoorLib.Factories;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;
using System.Xml;

namespace Door2DoorLib.Adapters
{
    internal class MySqlDatabase : Database
    {
        #region Fields
        private MySqlConnection _mySqlConnection;
        #endregion

        #region Constructor
        public MySqlDatabase(IConfiguration configuration, string databaseName) : base(configuration, databaseName)
        {
            // Creating our database connection
            _mySqlConnection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion

        #region Methods
        #region Open Connection Async
        // Opens the database connection
        public override async Task<bool> OpenConnectionAsync()
        {
            try
            {
                if (_mySqlConnection.State == ConnectionState.Open)
                {
                    return await Task.FromResult(true);
                }

                await _mySqlConnection.OpenAsync();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                LogFactory.CreateLog(LogTypes.File, $"Failed to open database connection due to {e.Message}", MessageTypes.Error).WriteLog();
                return await Task.FromResult(false);
            }

        }
        #endregion  

        #region Close Connection
        // Closes the database connection
        public override void CloseConnection()
        {
            try
            {
                _mySqlConnection.Close();
            }
            catch (Exception e)
            {
                LogFactory.CreateLog(LogTypes.File, $"Failed to close database connection due to {e.Message}", MessageTypes.Error).WriteLog();
            }
        }
        #endregion

        #region Execute Command Async
        // Executes sql command
        public override async Task<DbDataReader> ExecuteQueryAsync(DbCommand sqlCommand, IDictionary<string, object> sqlParams = null)
        {
            try
            {
                using MySqlCommand commandObj = new()
                {
                    CommandText = sqlCommand.CommandText,
                    CommandType = sqlCommand.CommandType,
                    Connection = _mySqlConnection,
                };

                if (sqlParams != null)
                {
                    AddSqlParamsToSqlCommand(commandObj, sqlParams);
                }

                await OpenConnectionAsync();

                return await commandObj.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                LogFactory.CreateLog(LogTypes.File, $"Failed to execute sql command due to {e.Message}", MessageTypes.Error).WriteLog();
                return await Task.FromResult<DbDataReader?>(null);
            }
        }
        #endregion

        private static void AddSqlParamsToSqlCommand(MySqlCommand commandObj, IDictionary<string, object> sqlParams)
        {
            foreach (var param in sqlParams)
            {
                commandObj.Parameters.AddWithValue(param.Key, param.Value);
            }
        }
        #endregion
    }
}
