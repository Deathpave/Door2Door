using Door2DoorLib.DataModels;
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
                    client.UploadFile($"ftp://10.13.0.125//Videos/{fileName}.{fileExtension}", WebRequestMethods.Ftp.UploadFile);
                    return Task.FromResult($"ftp://10.13.0.125//Videos/{fileName}.{fileExtension}");
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
            DbCommand sqlCommand = new SqlCommand("spCreateRoute");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@startId", createEntity.StartLocation },
                {"@endId", createEntity.EndLocation },
                { "@newText", createEntity.Description},
                { "@videourl", createEntity.VideoUrl}
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
        #endregion

        #region Delete Async
        /// <summary>
        /// Deletes a route entity in the database
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Route deleteEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spDeleteRoute");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int returnValue = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@routeId", deleteEntity.Id }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);

            dataReader.Read();
            returnValue = dataReader.RecordsAffected;

            if (returnValue != 0)
            {
                return true;
            }
            else
            {
                return false;
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
            DbCommand sqlCommand = new SqlCommand("spGetAllRoutes");
            sqlCommand.CommandType = CommandType.StoredProcedure;

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand);

            if (dataReader.HasRows == false) return new List<Route>();

            List<Route> result = new List<Route>();

            while (await dataReader.ReadAsync())
            {
                Route newroute = new Route(dataReader.GetString("videoUrl"), dataReader.GetString("text"), dataReader.GetInt64("startLocation"), dataReader.GetInt64("endLocation"), dataReader.GetInt64("id"));
                result.Add(newroute);
            }
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
            DbCommand sqlCommand = new SqlCommand("spGetRouteById");
            sqlCommand.CommandType = CommandType.StoredProcedure;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@id", id }
            };
            Route result = null;

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand);

            if (dataReader.HasRows == false) return result;

            while (dataReader.Read())
            {
                result = new Route(dataReader.GetString("videoUrl"), dataReader.GetString("text"), dataReader.GetInt64("startLocation"), dataReader.GetInt64("endLocation"), dataReader.GetInt64("id"));
            }
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
            DbCommand sqlCommand = new SqlCommand("spUpdateRoute");
            sqlCommand.CommandType = CommandType.StoredProcedure;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@startId", updateEntity.StartLocation },
                {"@endId", updateEntity.EndLocation },
                { "@newText", updateEntity.Description},
                { "@videourl", updateEntity.VideoUrl}
            };

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

        #region Get by Locations
        /// <summary>
        /// Get a route entity with matching location ids
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <returns></returns>
        public async Task<Route> GetByLocationsAsync(long startLocation, long endLocation)
        {
            DbCommand sqlCommand = new SqlCommand("spGetRouteByLocations");
            sqlCommand.CommandType = CommandType.StoredProcedure;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@startId", startLocation },
                {"@endId", endLocation },
            };

            Route result = null;

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand);

            if (dataReader.HasRows == false) return result;

            while (dataReader.Read())
            {
                result = new Route(dataReader.GetString("videoUrl"), dataReader.GetString("text"), dataReader.GetInt64("startLocation"), dataReader.GetInt64("endLocation"), dataReader.GetInt64("id"));
            }
            return await Task.FromResult(result);
        }
        #endregion
        #endregion
    }
}
