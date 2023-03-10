using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace Door2DoorLib.Adapters
{
    internal abstract class Database : IDatabase
    {
        #region Fields
        private readonly IConfiguration _configuration;
        private readonly string _databaseName;
        #endregion

        #region Properties
        public IConfiguration Configuration { get { return _configuration; } }
        public string DatabaseName { get { return _databaseName; } }
        #endregion

        #region Constructor
        public Database(IConfiguration configuration, string databaseName)
        {
            _configuration = configuration;
            _databaseName = databaseName;
        }
        #endregion

        #region Methods
        #region Close Connection
        // Default close connection method (not in use when overriding from other classes)
        public virtual Task<bool> CloseConnection()
        {
            LogFactory.CreateLog(LogTypes.Console, "Tried to close connection via abstract database class", MessageTypes.Error).WriteLog();
            return Task.FromResult(false);
        }
        #endregion

        #region Open Connection Async
        // Default open connection method (not in use when overriding from other classes)
        public virtual Task<bool> OpenConnectionAsync()
        {
            LogFactory.CreateLog(LogTypes.Console, "Tried to open connection via abstract database class", MessageTypes.Error).WriteLog();
            return Task.FromResult(false);
        }
        #endregion

        #region Execute Command Async
        // Default execute sql command (not in use when overriding from other classes)
        public virtual Task<DbDataReader> ExecuteQueryAsync(DbCommand sqlCommand, IDictionary<string, object> sqlParams = null)
        {
            LogFactory.CreateLog(LogTypes.Console, "Tried to execute sql command via abstract database class", MessageTypes.Error).WriteLog();
            return Task.FromResult<DbDataReader>(null);
        }

        #endregion
        #endregion
    }
}
