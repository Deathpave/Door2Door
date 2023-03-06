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
            string query = $"INSERT INTO routes (text,videoId) VALUES ({createEntity.Description},{createEntity.VideoId})";
            MySqlCommand sqlCommand = new MySqlCommand(query);

            var result = _database.ExecuteCommandAsync(sqlCommand);
            return Task.FromResult(false);
        }
        #endregion

        #region Delete Async
        // Deletes route row from id
        public Task<bool> DeleteAsync(Route deleteEntity)
        {
            string query = $"DELETE FROM routes WHERE id='{deleteEntity.Id}'";
            MySqlCommand sqlCommand = new MySqlCommand(query);
            var result = _database.ExecuteCommandAsync(sqlCommand);
            return Task.FromResult(true);

        }
        #endregion

        // TODO need name as well / instead of id
        public Task<Route> GetByIdAsync(long id)
        {
            string query = $"SELECT FROM routes WHERE id='{id}'";
            MySqlCommand sqlCommand = new MySqlCommand(query);
            Route result;
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                // Create a new route from the datastream
                result = new Route(streamReader.GetInt64("id"), streamReader.GetInt64("videoId"), streamReader.GetString("text"));
            }
            return Task.FromResult(result);
        }

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
                    result.Append(new Route(streamReader.GetInt64("id"), streamReader.GetInt64("videoId"), streamReader.GetString("text")));
                    streamReader.Read();
                }
            }
            return Task.FromResult(result);
        }

        // Updates route row 
        public Task<bool> UpdateAsync(Route updateEntity)
        {
            string query = $"UPDATE routes SET text = '{updateEntity.Description}',videoId='{updateEntity.Id}' WHERE id='{updateEntity.Id}'";
            MySqlCommand sqlCommand = new MySqlCommand(query);

            return _database.ExecuteCommandAsync(sqlCommand);
        }
        #endregion
    }
}
