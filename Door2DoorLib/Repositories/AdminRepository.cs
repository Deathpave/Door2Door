using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Security;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

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
            DbCommand sqlCommand = new SqlCommand("spCreateAdmin");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@username", new Encryption().EncryptString(createEntity.UserName, createEntity.UserName)));
            sqlCommand.Parameters.Add(new SqlParameter("@password", new Hashing().Sha256Hash(new Encryption().EncryptString(createEntity.Password, createEntity.Password))));

            await _database.OpenConnectionAsync();
            var result = _database.ExecuteQueryAsync(sqlCommand).Status;
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
            DbCommand sqlCommand = new SqlCommand("spDeleteAdmin");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@adminId", deleteEntity.Id));

            await _database.OpenConnectionAsync();
            var result = _database.ExecuteQueryAsync(sqlCommand).Status;
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
            DbCommand sqlCommand = new SqlCommand("spGetAdminById");
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@adminId", id));

            Admin result = null;
            await _database.OpenConnectionAsync();
            using (var streamReader = _database.ExecuteQueryAsync(sqlCommand).Result)
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
            await _database.OpenConnectionAsync();
            using (DbDataReader streamReader = _database.ExecuteQueryAsync(sqlCommand).Result)
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
            DbCommand sqlCommand = new SqlCommand("spUpdateAdmin");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@adminId", updateEntity.Id));
            sqlCommand.Parameters.Add(new SqlParameter("@username", new Encryption().EncryptString(updateEntity.UserName, updateEntity.UserName)));
            sqlCommand.Parameters.Add(new SqlParameter("@password", new Hashing().Sha256Hash(new Encryption().EncryptString(updateEntity.Password, updateEntity.Password))));

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
        #endregion
    }
}
