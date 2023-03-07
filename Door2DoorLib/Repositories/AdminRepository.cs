using Door2DoorLib.DataModels;
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
        // Creates new admin row
        public Task<bool> CreateAsync(Admin createEntity)
        {
            string query = $"INSERT INTO admins (username,password) VALUES ({createEntity.UserName},{createEntity.Password})";
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
        // Deletes admin row from id
        public Task<bool> DeleteAsync(Admin deleteEntity)
        {
            string query = $"DELETE FROM admins WHERE id='{deleteEntity.Id}'";
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

        #region Get By Name Async
        // Gets admin by name
        public Task<Admin> GetByNameAsync(string name)
        {
            string query = $"SELECT * FROM admis WHERE name='{name}'";
            MySqlCommand sqlCommand = new MySqlCommand(query);
            Admin result;
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                // Create a new admin from the datastream
                result = new Admin(streamReader.GetInt64("id"), streamReader.GetString("username"), streamReader.GetString("password"));
            }
            return Task.FromResult(result);
        }
        #endregion

        #region Udate Async
        // Updates admin
        public Task<bool> UpdateAsync(Admin updateEntity)
        {
            string query = $"UPDATE admins SET username = '{updateEntity.UserName}',password='{updateEntity.Password}' WHERE id='{updateEntity.Id}'";
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
