using System.Data.Common;

namespace Door2DoorLib.Interfaces
{
    public interface IDatabase
    {
        Task<DbDataReader> ExecuteQueryAsync(DbCommand sqlCommand, IDictionary<string, object> sqlParams = null);
        Task<bool> OpenConnectionAsync();
        Task<bool> CloseConnection();
    }
}
