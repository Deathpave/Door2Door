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
        #region Get By User Name
        public Task<Admin> GetByUserName(string userName)
        {
            string query = $"";
            MySqlCommand sqlCommand = new MySqlCommand(query);
            Admin result = null;
            using (var streamReader = _database.ExecuteCommandAsync(sqlCommand).Result)
            {
                // Create a new admin from the datastream
                while (streamReader.HasRows)
                {
                    result = new Admin(0, streamReader.GetString("username"), streamReader.GetString("password"));
                    streamReader.Read();
                }
            }
            return Task.FromResult(result);
        }
        #endregion
        #endregion
    }
}
