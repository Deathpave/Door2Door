using MySql.Data.MySqlClient;

namespace Door2DoorLib.Interfaces
{
    public interface IDatabase
    {
        #region Methods
        Task<MySqlDataReader> ExecuteCommandAsync(MySqlCommand sqlCommand);
        Task<bool> OpenConnectionAsync();
        void CloseConnection();
        #endregion
    }
}
