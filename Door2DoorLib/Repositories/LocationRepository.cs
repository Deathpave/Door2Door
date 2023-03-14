using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Door2DoorLib.Repositories
{
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
        /// <returns></returns>
        public async Task<bool> CreateAsync(Location createEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spCreateLocation");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@newName", createEntity.Name));
            sqlCommand.Parameters.Add(new SqlParameter("@newIconUrl", createEntity.IconUrl));

            await _database.OpenConnectionAsync();
            var result = _database.ExecuteQueryAsync(sqlCommand).Status;
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

        /// <summary>
        /// Deletes a Location entity from the database
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Location deleteEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spDeleteLocation");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@locationId", deleteEntity.Id));

            await _database.OpenConnectionAsync();
            var result = _database.ExecuteQueryAsync(sqlCommand).Status;
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

        /// <summary>
        /// Returns all Location entities from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            DbCommand sqlCommand = new SqlCommand("spGetAllLocations");
            sqlCommand.CommandType = CommandType.StoredProcedure;

            List<Location> result = new List<Location>();
            await _database.OpenConnectionAsync();
            using (DbDataReader streamReader = _database.ExecuteQueryAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    while (streamReader.Read())
                    {
                        Location newLocation = new Location(streamReader.GetString("name"), streamReader.GetString("iconUrl"), streamReader.GetInt64("id"));
                        result.Add(newLocation);
                    }
                }
                else
                {
                    LogFactory.CreateLog(LogTypes.File, "Could not get all locations async", MessageTypes.Error).WriteLog();
                }
            }
            _database.CloseConnection();
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Returns a given Location entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Location> GetByIdAsync(long id)
        {
            DbCommand sqlCommand = new SqlCommand("spGetLocationById");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@LocationId", id));

            Location result = null;
            await _database.OpenConnectionAsync();
            using (var streamReader = _database.ExecuteQueryAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    result = new Location(streamReader.GetString("name"), streamReader.GetString("iconUrl"), streamReader.GetInt64("id"));
                }
                else
                {
                    LogFactory.CreateLog(LogTypes.File, $"Could not get location by id {id}", MessageTypes.Error);
                }
            }
            _database.CloseConnection();
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Updates a Location Entity in the database
        /// </summary>
        /// <param name="updateEntity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> UpdateAsync(Location updateEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spUpdateLocation");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@routeId", updateEntity.Id));
            sqlCommand.Parameters.Add(new SqlParameter("@newText", updateEntity.Name));
            sqlCommand.Parameters.Add(new SqlParameter("@videourl", updateEntity.IconUrl));

            if (_database.ExecuteQueryAsync(sqlCommand).Status == TaskStatus.RanToCompletion)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        #endregion
    }
}
