using Door2DoorLib.Interfaces;
using Door2DoorLib.Logs;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;

namespace Door2DoorLib.Repositories
{
    internal class DbLogRepository : IDbLogRepository
    {
        private readonly IDatabase _database;

        public DbLogRepository(IDatabase database)
        {
            _database = database;
        }


        public async Task<bool> CreateAsync(DatabaseLog createEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spCreateLog");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@type", createEntity.MessageType));
            sqlCommand.Parameters.Add(new SqlParameter("@description", createEntity.Message));
            sqlCommand.Parameters.Add(new SqlParameter("@timestamp", DateTime.Now));

            await _database.OpenConnectionAsync();
            var result = _database.ExecuteCommandAsync(sqlCommand).Status;
            _database.CloseConnection();
            if (result == TaskStatus.RanToCompletion)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteAsync(DatabaseLog deleteEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spDeleteLog");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@logId", deleteEntity.Id));

            await _database.OpenConnectionAsync();
            var result = _database.ExecuteCommandAsync(sqlCommand).Status;
            _database.CloseConnection();
            if (result == TaskStatus.RanToCompletion)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<DatabaseLog>> GetAllAsync()
        {
            DbCommand sqlCommand = new SqlCommand("spGetAllLogs");
            sqlCommand.CommandType = CommandType.StoredProcedure;

            List<DatabaseLog> result = new List<DatabaseLog>();
            await _database.OpenConnectionAsync();
            using (DbDataReader streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    while (streamReader.Read())
                    {
                        DatabaseLog newLog = new DatabaseLog(streamReader.GetInt64("id"), (MessageTypes)streamReader.GetInt32("type"), streamReader.GetString("message"), streamReader.GetDateTime("timestamp"));
                        result.Add(newLog);
                    }
                }
                else
                {
                    LogFactory.CreateLog(LogTypes.File, "Could not get all routes async", MessageTypes.Error).WriteLog();
                }
            }
            _database.CloseConnection();
            return await Task.FromResult(result);
        }

        public async Task<DatabaseLog> GetByIdAsync(long id)
        {
            DbCommand sqlCommand = new SqlCommand("spGetLogById");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@logId", id));

            DatabaseLog result = null;
            await _database.OpenConnectionAsync();
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    streamReader.Read();
                    result = new DatabaseLog(streamReader.GetInt64("id"), (MessageTypes)streamReader.GetInt32("type"), streamReader.GetString("message"), streamReader.GetDateTime("timestamp"));
                }
                else
                {
                    LogFactory.CreateLog(LogTypes.File, $"Could not get log by id {id}", MessageTypes.Error);
                }
            }
            _database.CloseConnection();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateAsync(DatabaseLog updateEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spUpdateLog");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@logId", updateEntity.Id));
            sqlCommand.Parameters.Add(new SqlParameter("@type", updateEntity.MessageType));
            sqlCommand.Parameters.Add(new SqlParameter("@description", updateEntity.Message));
            sqlCommand.Parameters.Add(new SqlParameter("@timestamp", DateTime.Now));


            if (_database.ExecuteCommandAsync(sqlCommand).Status == TaskStatus.RanToCompletion)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
    }
}
