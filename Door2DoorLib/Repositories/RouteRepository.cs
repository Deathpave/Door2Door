using Door2DoorLib.DataModels;
using Door2DoorLib.Interfaces;
using MySql.Data.MySqlClient;

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
        public Task<bool> CreateAsync(Route createEntity)
        {
            string query = $"INSERT INTO routes (text,videoUrl) VALUES ({createEntity.Description},{createEntity.VideoUrl})";
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

        #region Delete Async
        // Deletes route row from id
        public Task<bool> DeleteAsync(Route deleteEntity)
        {
            string query = $"DELETE FROM routes WHERE id='{deleteEntity.Id}'";
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

        #region Get By Id Async
        // Gets route by id
        public Task<Route> GetByIdAsync(long id)
        {
            string query = $"SELECT FROM routes WHERE id='{id}'";
            MySqlCommand sqlCommand = new MySqlCommand(query);
            Route result;
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                // Create a new route from the datastream
                result = new Route(streamReader.GetInt64("id"), streamReader.GetString("videoUrl"), streamReader.GetString("text"), streamReader.GetString("name"));
            }
            return Task.FromResult(result);
        }
        #endregion

        #region Get By Name Async
        // Gets route by name
        public Task<Route> GetByNameAsync(string name)
        {
            string query = $"SELECT FROM routes WHERE name='{name}'";
            MySqlCommand sqlCommand = new MySqlCommand(query);
            Route result;
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                // Create a new route from the datastream
                result = new Route(streamReader.GetInt64("id"), streamReader.GetString("videoUrl"), streamReader.GetString("text"), streamReader.GetString("name"));
            }
            return Task.FromResult(result);
        }
        #endregion

        #region Get All Async
        // Gets all routes
        public Task<IEnumerable<Route>> GetAllAsync()
        {
            string query = $"SELECT * FROM routes";
            MySqlCommand sqlCommand = new MySqlCommand(query);
            IEnumerable<Route> result = new List<Route>();
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                // Create a new route from the datastream
                while (streamReader.HasRows)
                {
                    streamReader.Read();
                    result.Append(new Route(streamReader.GetInt64("id"), streamReader.GetString("videoUrl"), streamReader.GetString("text"), streamReader.GetString("name")));
                }
            }
            return Task.FromResult(result);
        }
        #endregion

        #region Udate Async
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
