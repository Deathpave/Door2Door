using Door2DoorLib.DataModels;
using Door2DoorLib.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

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
        /// <summary>
        /// Uploads a given file to the FTP server
        /// </summary>
        /// <param name="file"></param>
        /// <returns>string of the path where the file was stored</returns>
        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            try
            {
                using var stream = File.OpenWrite($"C:\\Door2Door\\Videos\\{file.FileName}");
                await file.CopyToAsync(stream);
                return await Task.FromResult($"http://10.13.0.125//Videos/{file.FileName}");
            }
            catch (Exception)
            {
                return await Task.FromResult(string.Empty);
            }
        }
        #endregion

        #region Create Async
        /// <summary>
        /// Creates a route entity in the database
        /// </summary>
        /// <param name="createEntity"></param>
        /// <returns>true or false</returns>
        public async Task<bool> CreateAsync(Route createEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spCreateRoute");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                {"@routeId", createEntity.Id},
                { "@startId", createEntity.StartLocation },
                {"@endId", createEntity.EndLocation },
                { "@newText", createEntity.Description},
                { "@videourl", createEntity.VideoUrl}
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
        #endregion

        #region Delete Async
        /// <summary>
        /// Deletes a route entity in the database
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns>true or false</returns>
        public async Task<bool> DeleteAsync(Route deleteEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spDeleteRoute");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@routeId", deleteEntity.Id }
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

        #region Get All Async
        /// <summary>
        /// Get's all Route entities from the database.
        /// </summary>
        /// <returns>all Route entities from database</returns>
        public async Task<IEnumerable<Route>> GetAllAsync()
        {
            DbCommand sqlCommand = new SqlCommand("spGetAllRoutes");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            List<Route> result = new List<Route>();

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand);

            if (dataReader.HasRows == false) return new List<Route>();

            while (await dataReader.ReadAsync())
            {
                Route newroute = new Route(dataReader.GetString("videoUrl"), dataReader.GetString("text"), dataReader.GetInt64("startLocation"), dataReader.GetInt64("endLocation"), dataReader.GetInt64("id"));
                result.Add(newroute);
            }

            await _database.CloseConnectionAsync();
            return await Task.FromResult(result);
        }
        #endregion

        #region Get By Id Async
        /// <summary>
        /// Get's a specific Route entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a Route matching the given id</returns>
        public async Task<Route> GetByIdAsync(long id)
        {
            DbCommand sqlCommand = new SqlCommand("spGetRouteById");
            sqlCommand.CommandType = CommandType.StoredProcedure;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@routeId", id }
            };
            Route result = null;

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);

            if (dataReader.HasRows == false) return result;

            while (dataReader.Read())
            {
                result = new Route(dataReader.GetString("videoUrl"), dataReader.GetString("text"), dataReader.GetInt64("startLocation"), dataReader.GetInt64("endLocation"), dataReader.GetInt64("id"));
            }
            await _database.CloseConnectionAsync();
            return await Task.FromResult(result);
        }
        #endregion

        #region Update Async
        /// <summary>
        /// Updates a route entity in the database
        /// </summary>
        /// <param name="updateEntity"></param>
        /// <returns>True or False</returns>
        public async Task<bool> UpdateAsync(Route updateEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spUpdateRoute");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@routeId", updateEntity.Id },
                { "@startId", updateEntity.StartLocation },
                { "@endId", updateEntity.EndLocation },
                { "@newText", updateEntity.Description},
                { "@videourl", updateEntity.VideoUrl}
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

        #region Get by Locations
        /// <summary>
        /// Get a route entity with matching location ids
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <returns>A route object matching the location ids</returns>
        public async Task<Route> GetByLocationsAsync(long startLocation, long endLocation)
        {
            DbCommand sqlCommand = new SqlCommand("spGetRouteByLocations");
            sqlCommand.CommandType = CommandType.StoredProcedure;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@startId", startLocation },
                { "@endId", endLocation }
            };

            Route result = null;

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);

            if (dataReader.HasRows == false) return result;

            while (dataReader.Read())
            {
                result = new Route(dataReader.GetString("videoUrl"), dataReader.GetString("text"), dataReader.GetInt64("startLocation"), dataReader.GetInt64("endLocation"), dataReader.GetInt64("id"));
            }

            await _database.CloseConnectionAsync();
            return await Task.FromResult(result);
        }
        #endregion
        #endregion
    }
}
