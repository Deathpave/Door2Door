using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using MySql.Data.MySqlClient;

namespace Door2DoorLib.Repositories
{
    internal class AdminRepository : IAdminRepository
    {
        #region Fields
        private IDatabase _database;
        #endregion

        #region Constructor
        public AdminRepository(IDatabase database)
        {
            _database = database;
        }
        #endregion

        #region Methods
        #region Create Async
        /// <summary>
        /// Creates an Admin entity in the database
        /// </summary>
        /// <param name="createEntity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Admin createEntity)
        {
            MySqlCommand sqlCommand = new MySqlCommand("d2d.spCreateAdmin");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new MySqlParameter("@username", createEntity.UserName));
            sqlCommand.Parameters.Add(new MySqlParameter("@password", createEntity.Password));

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
        /// Deletes an Admin entity from the database
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Admin deleteEntity)
        {
            MySqlCommand sqlCommand = new MySqlCommand("spDeleteAdmin");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new MySqlParameter("@adminId", deleteEntity.Id));

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

        #region Get By Id
        /// <summary>
        /// Returns an Admin entity with matching id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Admin> GetByIdAsync(long id)
        {
            MySqlCommand sqlCommand = new MySqlCommand("spGetAdminById");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new MySqlParameter("@adminId", id));

            Admin result = null;
            await _database.OpenConnectionAsync();
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    result = new Admin(streamReader.GetInt64("id"), streamReader.GetString("username"), streamReader.GetString("password"));
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

        #region Get All
        /// <summary>
        /// Returns all Admin entities from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            MySqlCommand sqlCommand = new MySqlCommand("spGetAllRoutes");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            List<Admin> result = new List<Admin>();
            await _database.OpenConnectionAsync();
            using (MySqlDataReader streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    while (streamReader.Read())
                    {
                        Admin newroute = new Admin(streamReader.GetInt64("id"), streamReader.GetString("username"), streamReader.GetString("password"));
                        result.Add(newroute);
                    }
                }
                else
                {
                    LogFactory.CreateLog(LogTypes.File, "Could not get all admins", MessageTypes.Error).WriteLog();
                }
            }
            _database.CloseConnection();
            return await Task.FromResult(result);
        }
        #endregion

        #region Get By Name Async
        /// <summary>
        /// Returns an admin entity with matchin name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Admin> GetByNameAsync(string name)
        {
            MySqlCommand sqlCommand = new MySqlCommand("spGetAdminByName");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            Admin result = null;
            await _database.OpenConnectionAsync();
            using (MySqlDataReader streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                if (streamReader != null)
                {
                    // Create a new route from the datastream
                    while (streamReader.Read())
                    {
                        result = new Admin(streamReader.GetInt64("id"), streamReader.GetString("username"), streamReader.GetString("password"));
                    }
                }
                else
                {
                    LogFactory.CreateLog(LogTypes.File, "Could not get admin by name", MessageTypes.Error).WriteLog();
                }
            }
            _database.CloseConnection();
            return await Task.FromResult(result);
        }
        #endregion

        #region Update Async
        /// <summary>
        /// Updates an Admin entity in the database
        /// </summary>
        /// <param name="updateEntity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Admin updateEntity)
        {
            MySqlCommand sqlCommand = new MySqlCommand("spUpdateAdmin");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new MySqlParameter("@adminId", updateEntity.Id));
            sqlCommand.Parameters.Add(new MySqlParameter("@newUsername", updateEntity.UserName));
            sqlCommand.Parameters.Add(new MySqlParameter("@newPassword", updateEntity.Password));

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
        #endregion
    }
}
