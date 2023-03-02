using Door2DoorLib.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Door2DoorLib.Adapters
{
    internal abstract class Database : IDatabase
    {
        #region Fields
        private IConfiguration _configuration;
        private string _databaseName;
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
        #region Get Connection String
        public string GetConnectionString(string databaseName)
        {
            return _configuration.GetConnectionString(databaseName);
        }
        #endregion

        #region Close Connection
        // Default close connection method (not in use when overriding from other classes)
        public virtual void CloseConnection()
        {
            throw new Exception("Template dont hold a datebase connection");
        }
        #endregion

        #region Open Connection Async
        // Default open connection method (not in use when overriding from other classes)
        public virtual Task<bool> OpenConnectionAsync()
        {
            throw new Exception("Template dont hold a datebase connection");
        }
        #endregion

        #region Execute Command Async
        // Default execute sql command (not in use when overriding from other classes)
        public virtual Task<MySqlDataReader> ExecuteCommandAsync(MySqlCommand sqlCommand)
        {
            throw new Exception("Template dont hold a database connection");
        }
        #endregion
        #endregion
    }
}
