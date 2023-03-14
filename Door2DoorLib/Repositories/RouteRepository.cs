using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;

namespace Door2DoorLib.Repositories
{
    internal class RouteRepository : IRouteRepository
    {
        #region Fields
        private readonly IDatabase _database;
        #endregion

        #region Constructor
        public RouteRepository(IDatabase database)
        {
            _database = database;
        }
        #endregion

        #region Methods
        #region Upload Video
        public Task<string> UploadVideo(string fileName, string fileExtension)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential("Administrator", "Kode1234!");
                    client.UploadFile($"ftp://10.13.0.125//path/{fileName}.{fileExtension}", WebRequestMethods.Ftp.UploadFile);
                    return Task.FromResult($"ftp://10.13.0.125//path/{fileName}.{fileExtension}");
                }
            }
            catch (Exception)
            {
                return Task.FromResult(string.Empty);
            }
        }
        #endregion

        #region Create Async

        /// <summary>
        /// Creates a route entity in the database
        /// </summary>
        /// <param name="createEntity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Route createEntity)
        {
            //MySqlCommand sqlCommand = new MySqlCommand("spCreateRoute");
            //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //sqlCommand.Parameters.Add(new MySqlParameter("@newText", createEntity.Description));
            //sqlCommand.Parameters.Add(new MySqlParameter("@videourl", createEntity.VideoUrl));
            //sqlCommand.Parameters.Add(new MySqlParameter("@startId", createEntity.StartLocation));
            //sqlCommand.Parameters.Add(new MySqlParameter("@endId", createEntity.EndLocation));

            DbCommand sqlCommand = new SqlCommand("spCreateRoute");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@newText", createEntity.Description));
            sqlCommand.Parameters.Add(new SqlParameter("@videourl", createEntity.VideoUrl));
            sqlCommand.Parameters.Add(new SqlParameter("@startId", createEntity.StartLocation));
            sqlCommand.Parameters.Add(new SqlParameter("@endId", createEntity.EndLocation));

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
        #endregion

        #region Delete Async
        /// <summary>
        /// Deletes a route entity in the database
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Route deleteEntity)
        {
            //MySqlCommand sqlCommand = new MySqlCommand("spDeleteRoute");
            //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //sqlCommand.Parameters.Add(new MySqlParameter("@routeId", deleteEntity.Id));

            DbCommand sqlCommand = new SqlCommand("spDeleteRoute");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@routeId", deleteEntity.Id));

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
        #endregion

        #region Get All Async
        /// <summary>
        /// Returns all Route entities from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Route>> GetAllAsync()
        {
            //MySqlCommand sqlCommand = new MySqlCommand("spGetAllRoutes");
            //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            DbCommand sqlCommand = new SqlCommand("spGetAllRoutes");
            sqlCommand.CommandType = CommandType.StoredProcedure;

            List<Route> result = new List<Route>();
            await _database.OpenConnectionAsync();
            using (DbDataReader streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    while (streamReader.Read())
                    {
                        Route newroute = new Route(streamReader.GetInt64("id"), streamReader.GetString("videoUrl"), streamReader.GetString("text"), streamReader.GetInt64("startLocation"), streamReader.GetInt64("endLocation"));
                        result.Add(newroute);
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
        #endregion

        #region Get By Id Async
        /// <summary>
        /// Returns a Route entity matching the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Route> GetByIdAsync(long id)
        {
            //MySqlCommand sqlCommand = new MySqlCommand("spGetRouteById");
            //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //sqlCommand.Parameters.Add(new MySqlParameter("@routeId", id));

            DbCommand sqlCommand = new SqlCommand("spGetRouteById");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@routeId", id));

            Route result = null;
            await _database.OpenConnectionAsync();
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    streamReader.Read();
                    result = new Route(streamReader.GetInt64("id"), streamReader.GetString("videoUrl"), streamReader.GetString("text"), streamReader.GetInt64("startLocation"), streamReader.GetInt64("endLocation"));
                }
                else
                {
                    LogFactory.CreateLog(LogTypes.File, $"Could not get route by id {id}", MessageTypes.Error);
                }
            }
            _database.CloseConnection();
            return await Task.FromResult(result);
        }
        #endregion

        #region Update Async
        /// <summary>
        /// Updates a route entity in the database
        /// </summary>
        /// <param name="updateEntity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Route updateEntity)
        {
            //MySqlCommand sqlCommand = new MySqlCommand("spUpdateRoute");
            //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //sqlCommand.Parameters.Add(new MySqlParameter("@routeId", updateEntity.Id));
            //sqlCommand.Parameters.Add(new MySqlParameter("@newText", updateEntity.Description));
            //sqlCommand.Parameters.Add(new MySqlParameter("@videourl", updateEntity.VideoUrl));
            //sqlCommand.Parameters.Add(new MySqlParameter("@startId", updateEntity.StartLocation));
            //sqlCommand.Parameters.Add(new MySqlParameter("@endId", updateEntity.EndLocation));

            DbCommand sqlCommand = new SqlCommand("spUpdateRoute");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@routeId", updateEntity.Id));
            sqlCommand.Parameters.Add(new SqlParameter("@newText", updateEntity.Description));
            sqlCommand.Parameters.Add(new SqlParameter("@videourl", updateEntity.VideoUrl));
            sqlCommand.Parameters.Add(new SqlParameter("@startId", updateEntity.StartLocation));
            sqlCommand.Parameters.Add(new SqlParameter("@endId", updateEntity.EndLocation));


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

        #region Get by Locations
        /// <summary>
        /// Get a route entity with matching location ids
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <returns></returns>
        public async Task<Route> GetByLocationsAsync(long startLocation, long endLocation)
        {
            //MySqlCommand sqlCommand = new MySqlCommand("spGetRouteByLocations");
            //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //sqlCommand.Parameters.Add(new MySqlParameter("@startId", startLocation));
            //sqlCommand.Parameters.Add(new MySqlParameter("@endId", endLocation));

            DbCommand sqlCommand = new SqlCommand("spGetRouteByLocations");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@startId", startLocation));
            sqlCommand.Parameters.Add(new SqlParameter("@endId", endLocation));

            Route result = null;
            await _database.OpenConnectionAsync();
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    streamReader.Read();
                    result = new Route(streamReader.GetInt64("id"), streamReader.GetString("videoUrl"), streamReader.GetString("text"), streamReader.GetInt64("startLocation"), streamReader.GetInt64("endLocation"));
                }
                else
                {
                    LogFactory.CreateLog(LogTypes.File, $"Could not get route by ids {startLocation}-{endLocation}", MessageTypes.Error);
                }
            }
            _database.CloseConnection();
            return await Task.FromResult(result);
        }
        #endregion
        #endregion
    }
}
