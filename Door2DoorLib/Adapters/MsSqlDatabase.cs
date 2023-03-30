using Door2DoorLib.Factories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Door2DoorLib.Adapters
{
    internal class MsSqlDatabase : Database
    {
        #region Fields
        private readonly SqlConnection _sqlConnection;
        #endregion

        #region Constructor
        public MsSqlDatabase(IConfiguration configuration, string databaseName) : base(configuration, databaseName)
        {
            // Creating our database connection
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion

        #region Methods
        #region Open Connection Async
        /// <summary>
        /// Opens the database connection
        /// </summary>
        /// <returns>True or False</returns>
        public override async Task<bool> OpenConnectionAsync()
        {
            try
            {
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    return await Task.FromResult(true);
                }

                await _sqlConnection.OpenAsync();
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
        /// <summary>
        /// Closes the database connection
        /// </summary>
        /// <returns>True or False</returns>
        public override async Task<bool> CloseConnectionAsync()
        {
            try
            {
                if (_sqlConnection.State == ConnectionState.Closed)
                {
                    return await Task.FromResult(true);
                }

                await _sqlConnection.CloseAsync();
                return await Task.FromResult(true);

            }
            catch (Exception e)
            {
                LogFactory.CreateLog(LogTypes.File, $"Failed to close database connection due to {e.Message}", MessageTypes.Error).WriteLog();
                return await Task.FromResult(false);
            }
        }
        #endregion

        #region Execute Command Async
        /// <summary>
        /// Executes sql command
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="sqlParams"></param>
        /// <returns>DbDataReader</returns>
        public override async Task<DbDataReader> ExecuteQueryAsync(DbCommand sqlCommand, IDictionary<string, object> sqlParams = null)
        {
            try
            {
                using SqlCommand commandObj = new()
                {
                    CommandText = sqlCommand.CommandText,
                    CommandType = sqlCommand.CommandType,
                    Connection = _sqlConnection,
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

        /// <summary>
        /// Adds parameters in an IDictionary to a MySqlCommand object
        /// </summary>
        /// <param name="commandObj"></param>
        /// <param name="sqlParams"></param>
        private static void AddSqlParamsToSqlCommand(SqlCommand commandObj, IDictionary<string, object> sqlParams)
        {
            foreach (var param in sqlParams)
            {
                commandObj.Parameters.AddWithValue(param.Key, param.Value);
            }
        }
        #endregion
    }
}
