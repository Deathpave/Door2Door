using Door2DoorLib.DataModels;
using Door2DoorLib.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Door2DoorLib.Repositories
{
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
        /// <returns></returns>
        public async Task<bool> CreateAsync(Admin createEntity)
        {
            DbCommand sqlCommand = new SqlCommand("spCreateAdmin");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                {"@username", createEntity.UserName },
                {"@password", createEntity.Password }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);
            dataReader.Read();
            affectedRows = dataReader.RecordsAffected;
            await _database.CloseConnection();

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
        /// <returns></returns>
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
            await _database.CloseConnection();

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
        /// Returns an Admin entity with matching id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                result = new Admin(dataReader.GetString("username"), dataReader.GetString("password"));
            }
            await _database.CloseConnection();

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
            throw new NotImplementedException("This is not allowed");
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
                result = new Admin(dataReader.GetString("username"), dataReader.GetString("password"));
            }
            await _database.CloseConnection();

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
            DbCommand sqlCommand = new SqlCommand("spUpdateAdmin");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int affectedRows = 0;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@adminId", updateEntity.Id },
                { "@username", updateEntity.UserName },
                { "@password", updateEntity.Password }
            };

            using var dataReader = await _database.ExecuteQueryAsync(sqlCommand, sqlParams);
            dataReader.Read();
            affectedRows = dataReader.RecordsAffected;
            await _database.CloseConnection();

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
