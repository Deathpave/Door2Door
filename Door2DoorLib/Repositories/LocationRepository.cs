using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using MySql.Data.MySqlClient;

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
            MySqlCommand sqlCommand = new MySqlCommand("spCreateLocation");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new MySqlParameter("@newName", createEntity.Name));
            sqlCommand.Parameters.Add(new MySqlParameter("@newIconUrl", createEntity.IconUrl));

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

        /// <summary>
        /// Deletes a Location entity from the database
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Location deleteEntity)
        {
            MySqlCommand sqlCommand = new MySqlCommand("spDeleteLocation");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new MySqlParameter("@locationId", deleteEntity.Id));

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

        /// <summary>
        /// Returns all Location entities from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            MySqlCommand sqlCommand = new MySqlCommand("spGetAllLocations");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            List<Location> result = new List<Location>();
            await _database.OpenConnectionAsync();
            using (MySqlDataReader streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    while (streamReader.Read())
                    {
                        Location newLocation = new Location(streamReader.GetInt64("id"), streamReader.GetString("name"), streamReader.GetString("iconUrl"));
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
            MySqlCommand sqlCommand = new MySqlCommand("spGetLocationById");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new MySqlParameter("@LocationId", id));

            Location result = null;
            await _database.OpenConnectionAsync();
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    result = new Location(streamReader.GetInt64("id"), streamReader.GetString("name"), streamReader.GetString("iconUrl"));
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
            MySqlCommand sqlCommand = new MySqlCommand("spUpdateLocation");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new MySqlParameter("@routeId", updateEntity.Id));
            sqlCommand.Parameters.Add(new MySqlParameter("@newText", updateEntity.Name));
            sqlCommand.Parameters.Add(new MySqlParameter("@videourl", updateEntity.IconUrl));

            if (_database.ExecuteCommandAsync(sqlCommand).Status == TaskStatus.RanToCompletion)
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
