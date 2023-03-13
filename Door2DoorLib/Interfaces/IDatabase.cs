using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Door2DoorLib.Interfaces
{
    public interface IDatabase
    {
        #region Methods
        Task<DbDataReader> ExecuteCommandAsync(DbCommand sqlCommand);
        Task<bool> OpenConnectionAsync();
        void CloseConnection();
        #endregion
    }
}
