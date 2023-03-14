using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Logs;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

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
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@type", createEntity.MessageType },
                { "@description", createEntity.Message },
                { "@timestamp", createEntity.TimeStamp }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);
            dataReader.Read();
            affectedRows = dataReader.RecordsAffected;

            if (affectedRows > 0)
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
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@logId", deleteEntity.Id }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);
            dataReader.Read();
            affectedRows = dataReader.RecordsAffected;

            if (affectedRows != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<DatabaseLog>> GetAllAsync()
        {
            DbCommand sqlCommand = new SqlCommand("spGetAllLogs");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            List<DatabaseLog> result = new List<DatabaseLog>();

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand);

            if (dataReader.HasRows == false)
            {
                return new List<DatabaseLog>();
            }

            while (await dataReader.ReadAsync())
            {
                DatabaseLog newLog = new DatabaseLog(dataReader.GetInt64("id"), (MessageTypes)dataReader.GetInt32("type"), dataReader.GetString("message"), dataReader.GetDateTime("timestamp"));
                result.Add(newLog);
            }
            return await Task.FromResult(result);

        }

        public async Task<DatabaseLog> GetByIdAsync(long id)
        {
            DbCommand sqlCommand = new SqlCommand("spGetLogById");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            DatabaseLog result = null;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@logId", id }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);

            if (dataReader.HasRows == false)
            {
                return result;
            }

            while (dataReader.Read())
            {
                result = new DatabaseLog(dataReader.GetInt64("id"), (MessageTypes)dataReader.GetInt32("type"), dataReader.GetString("message"), dataReader.GetDateTime("timestamp"));
            }
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
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@logId", updateEntity.Id },
                { "@type", updateEntity.MessageType },
                { "@description", updateEntity.Message },
                { "@timestamp", updateEntity.TimeStamp }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);
            dataReader.Read();
            affectedRows = dataReader.RecordsAffected;

            if (affectedRows != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
