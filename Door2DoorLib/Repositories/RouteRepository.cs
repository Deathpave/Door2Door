using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Door2DoorLib.Repositories
{
    internal class RouteRepository : IRouteRepository
    {
        #region Fields
        private IDatabase _database;
        #endregion

        #region Constructor
        public RouteRepository(IDatabase database)
        {
            _database = database;
        }
        #endregion

        #region Methods
        #region Create Async
        // Creates new route row
        public async Task<bool> CreateAsync(Route createEntity)
        {
            string query = $"INSERT INTO routes (text,videoUrl) VALUES ({createEntity.Description},{createEntity.VideoUrl})";
            MySqlCommand sqlCommand = new MySqlCommand(query);

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
        // Deletes route row from id
        public async Task<bool> DeleteAsync(Route deleteEntity)
        {
            string query = $"DELETE FROM routes WHERE id='{deleteEntity.Id}'";
            MySqlCommand sqlCommand = new MySqlCommand(query);
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

        #region Get By Id Async
        // Gets route by id
        public async Task<Route> GetByIdAsync(long id)
        {
            string query = $"SELECT FROM routes WHERE id='{id}'";
            MySqlCommand sqlCommand = new MySqlCommand(query);
            Route result = null;
            await _database.OpenConnectionAsync();
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    result = new Route(streamReader.GetInt64("id"), streamReader.GetString("videoUrl"), streamReader.GetString("text"), streamReader.GetString("name"));
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

        #region Get By Name Async
        // Gets route by name
        public async Task<Route> GetByNameAsync(string name)
        {
            string query = $"SELECT FROM routes WHERE name='{name}'";
            MySqlCommand sqlCommand = new MySqlCommand(query);
            Route result = null;
            await _database.OpenConnectionAsync();
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    result = new Route(streamReader.GetInt64("id"), streamReader.GetString("videoUrl"), streamReader.GetString("text"), streamReader.GetString("name"));
                }
                else
                {
                    LogFactory.CreateLog(LogTypes.File, $"Could not get route by name {name}", MessageTypes.Error);
                }
            }
            _database.CloseConnection();
            return await Task.FromResult(result);
        }
        #endregion

        #region Get All Async
        // Gets all routes
        public async Task<IEnumerable<Route>> GetAllAsync()
        {
            string query = $"SELECT * FROM routes";
            MySqlCommand sqlCommand = new MySqlCommand(query);
            List<Route> result = new List<Route>();
            await _database.OpenConnectionAsync();
            using (MySqlDataReader streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    while (streamReader.Read())
                    {
                        Route newroute = new Route(streamReader.GetInt64("id"), streamReader.GetString("videoUrl"), streamReader.GetString("text"), streamReader.GetString("name"));
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

        #region Update Async
        // Updates route
        public Task<bool> UpdateAsync(Route updateEntity)
        {
            string query = $"UPDATE routes SET text = '{updateEntity.Description}',videoUrl='{updateEntity.Id}' WHERE id='{updateEntity.Id}'";
            MySqlCommand sqlCommand = new MySqlCommand(query);

            if (_database.ExecuteCommandAsync(sqlCommand).Status == TaskStatus.RanToCompletion)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
        #endregion
        #endregion
    }
}
