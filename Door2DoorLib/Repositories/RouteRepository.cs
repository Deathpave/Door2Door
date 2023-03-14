using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Prng;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;
using System.Xml;

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
        public Task<string> UploadVideo(string filePath, string fileName, string fileExtension)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential("Administrator", "Kode1234!");
                    client.UploadFile($"ftp://10.13.0.125//path/{fileName}.{fileExtension}", WebRequestMethods.Ftp.UploadFile, filePath);
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
            //MySqlCommand sqlCommand = new MySqlCommand("spDeleteRoute");
            //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //sqlCommand.Parameters.Add(new MySqlParameter("@routeId", deleteEntity.Id));

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
            //MySqlCommand sqlCommand = new MySqlCommand("spGetAllRoutes");
            //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            DbCommand sqlCommand = new SqlCommand("spGetAllRoutes");
            sqlCommand.CommandType = CommandType.StoredProcedure;

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand);

            if (dataReader.HasRows == false) return new List<Route>();

            List<Route> result = new List<Route>();

            while (await dataReader.ReadAsync())
            {
                Route newroute = new Route(dataReader.GetInt64("id"), dataReader.GetString("videoUrl"), dataReader.GetString("text"), dataReader.GetInt64("startLocation"), dataReader.GetInt64("endLocation"));
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
            //MySqlCommand sqlCommand = new MySqlCommand("spGetRouteById");
            //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //sqlCommand.Parameters.Add(new MySqlParameter("@routeId", id));

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
                result = new Route(dataReader.GetInt64("id"), dataReader.GetString("videoUrl"), dataReader.GetString("text"), dataReader.GetInt64("startLocation"), dataReader.GetInt64("endLocation"));
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
            //MySqlCommand sqlCommand = new MySqlCommand("spUpdateRoute");
            //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //sqlCommand.Parameters.Add(new MySqlParameter("@routeId", updateEntity.Id));
            //sqlCommand.Parameters.Add(new MySqlParameter("@newText", updateEntity.Description));
            //sqlCommand.Parameters.Add(new MySqlParameter("@videourl", updateEntity.VideoUrl));
            //sqlCommand.Parameters.Add(new MySqlParameter("@startId", updateEntity.StartLocation));
            //sqlCommand.Parameters.Add(new MySqlParameter("@endId", updateEntity.EndLocation));

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
            //MySqlCommand sqlCommand = new MySqlCommand("spGetRouteByLocations");
            //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //sqlCommand.Parameters.Add(new MySqlParameter("@startId", startLocation));
            //sqlCommand.Parameters.Add(new MySqlParameter("@endId", endLocation));

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
                result = new Route(dataReader.GetInt64("id"), dataReader.GetString("videoUrl"), dataReader.GetString("text"), dataReader.GetInt64("startLocation"), dataReader.GetInt64("endLocation"));
            }
            return await Task.FromResult(result);
        }
        #endregion
        #endregion
    }
}
