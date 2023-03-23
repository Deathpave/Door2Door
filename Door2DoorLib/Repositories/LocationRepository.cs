using Door2DoorLib.DataModels;
using Door2DoorLib.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Door2DoorLib.Repositories
{
    /// <summary>
    /// Repository class for handling all database call regarding Location entities
    /// </summary>
    internal class LocationRepository : ILocationRepository
    {
        #region Fields
        private readonly IDatabase _database;
        #endregion

        #region Constructor
        public LocationRepository(IDatabase database)
        {
            _database = database;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Creates a new Location entity in the database
        /// </summary>
        /// <param name="createEntity"></param>
        /// <returns>True or False</returns>
        public async Task<bool> CreateAsync(Location createEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spCreateLocation");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                {"@locationId", createEntity.Id },
                { "@newName", createEntity.Name },
                { "@newIconUrl", createEntity.IconUrl }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);
            dataReader.Read();
            affectedRows = dataReader.RecordsAffected;
            await _database.CloseConnectionAsync();

            if (affectedRows > 0)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        /// <summary>
        /// Deletes a Location entity from the database
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns>True or False</returns>
        public async Task<bool> DeleteAsync(Location deleteEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spDeleteLocation");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@locationId", deleteEntity.Id }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);
            dataReader.Read();
            affectedRows = dataReader.RecordsAffected;
            await _database.CloseConnectionAsync();

            if (affectedRows != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get's all Location entities from the database
        /// </summary>
        /// <returns>All Locations</returns>
        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            DbCommand sqlCommand = new SqlCommand("spGetAllLocations");
            sqlCommand.CommandType = CommandType.StoredProcedure;

            List<Location> result = new List<Location>();

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand);

            if (dataReader.HasRows == false)
            {
                return new List<Location>();
            }

            while (await dataReader.ReadAsync())
            {
                Location newLocation = new Location(dataReader.GetString("name"), dataReader.GetString("iconUrl"), dataReader.GetInt64("id"));
                result.Add(newLocation);
            }
            await _database.CloseConnectionAsync();
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Get's a specific Location from it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Location object matching the given id</returns>
        public async Task<Location> GetByIdAsync(long id)
        {
            DbCommand sqlCommand = new SqlCommand("spGetLocationById");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            Location result = null;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@locationId", id }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);

            if (dataReader.HasRows == false)
            {
                return result;
            }

            while (dataReader.Read())
            {
                result = new Location(dataReader.GetString("name"), dataReader.GetString("iconUrl"), dataReader.GetInt64("id"));
            }
            await _database.CloseConnectionAsync();
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Updates a Location Entity in the database
        /// </summary>
        /// <param name="updateEntity"></param>
        /// <returns>True or False</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> UpdateAsync(Location updateEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spUpdateLocation");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@locationId", updateEntity.Id },
                { "@newName", updateEntity.Name },
                { "@newIconUrl", updateEntity.IconUrl }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);
            dataReader.Read();
            affectedRows = dataReader.RecordsAffected;
            await _database.CloseConnectionAsync();

            if (affectedRows != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
