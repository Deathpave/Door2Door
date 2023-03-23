using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Door2DoorLib.Repositories
{
    /// <summary>
    /// Repository class for handling all database call regarding Admin entities
    /// </summary>
    internal class AdminRepository : IAdminRepository
    {
        #region Fields
        private readonly IDatabase _database;
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
        /// <returns>True or False</returns>
        public async Task<bool> CreateAsync(Admin createEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spCreateAdmin");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int affectedRows = 0;
            IDictionary<string, object> sqlParams = new Dictionary<string, object>();

            sqlParams = new Dictionary<string, object>
            {
                {"@adminId", createEntity.Id },
                {"@username", createEntity.UserName },
                {"@password", createEntity.Password }
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
        /// Deletes an Admin entity from the database
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns>True or False</returns>
        public async Task<bool> DeleteAsync(Admin deleteEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spDeleteAdmin");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@adminId", deleteEntity.Id }
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

        #region Get By Id
        /// <summary>
        /// Get's an Admin entity with matching id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Admin object with matching id</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Admin> GetByIdAsync(long id)
        {
            DbCommand sqlCommand = new SqlCommand("spGetAdminById");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            Admin result = null;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@adminId", id }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);

            if (dataReader.HasRows == false)
            {
                return result;
            }

            while (dataReader.Read())
            {
                result = AdminFactory.CreateAdmin(dataReader.GetString("username"), dataReader.GetString("password"));
            }
            await _database.CloseConnectionAsync();

            return await Task.FromResult(result);
        }
        #endregion

        #region Get All
        /// <summary>
        /// Get's all Admin entities from the database
        /// </summary>
        /// <returns>All Admin objects</returns>
        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            DbCommand sqlCommand = new SqlCommand("spGetAllAdmins");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            List<Admin> result = new List<Admin>();

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand);

            if (dataReader.HasRows == false) return new List<Admin>();

            while (await dataReader.ReadAsync())
            {
                Admin newroute = AdminFactory.CreateAdmin(dataReader.GetString("username"), dataReader.GetString("password"), dataReader.GetInt64("id"));
                result.Add(newroute);
            }

            await _database.CloseConnectionAsync();
            return await Task.FromResult(result);
        }
        #endregion

        #region Get By Name Async
        /// <summary>
        /// Get's an admin entity with matchin name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>An Admin with matchin name</returns>
        public async Task<Admin> GetByNameAsync(string name)
        {
            DbCommand sqlCommand = new SqlCommand("spGetAdminByName");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            Admin result = null;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@adminName", name }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);

            if (dataReader.HasRows == false)
            {
                return result;
            }

            while (dataReader.Read())
            {
                result = AdminFactory.CreateAdmin(dataReader.GetString("username"), dataReader.GetString("password"), dataReader.GetInt64("id"));
            }
            await _database.CloseConnectionAsync();

            return await Task.FromResult(result);

        }
        #endregion

        #region Update Async
        /// <summary>
        /// Updates an Admin entity in the database
        /// </summary>
        /// <param name="updateEntity"></param>
        /// <returns>True or False</returns>
        public async Task<bool> UpdateAsync(Admin updateEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spUpdateAdmin");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@adminId", updateEntity.Id },
                { "@newUsername", updateEntity.UserName },
                { "@newPassword", updateEntity.Password }
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
        #endregion
    }
}
