using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Repositories;
using Door2DoorLib.Security;

namespace Door2DoorLib.Managers
{
    public class AdminManager : IAdminManager
    {
        #region Fields
        private AdminRepository _repository;
        #endregion

        #region Constructor
        public AdminManager(IDatabase database)
        {
            _repository = new AdminRepository(database);
        }
        #endregion

        #region Methods
        #region Check Login Async
        /// <summary>
        /// Validates admin login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pswd"></param>
        /// <returns></returns>
        public async Task<bool> CheckLoginAsync(string username, string pswd)
        {
            Admin admin = _repository.GetByNameAsync(new Encryption().EncryptString(username, username)).Result;
            if (admin.Password == new Hashing().Sha256Hash(new Encryption().EncryptString(pswd, pswd)))
            {
                return await Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"Failed login with username {username}", MessageTypes.Error);
                return await Task.FromResult(false);
            }
        }
        #endregion

        #region Add Admin Async
        /// <summary>
        /// Adds new admin, and logs the admin who did it
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="newAdmin"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Admin newAdmin, Admin admin)
        {
            if (_repository.CreateAsync(newAdmin).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} created a new admin user {newAdmin.UserName}", MessageTypes.Added).WriteLog();
                return await Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to create new admin user {newAdmin.UserName}", MessageTypes.Error).WriteLog();
                return await Task.FromResult(false);
            }
        }
        #endregion

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                LogFactory.CreateLog(LogTypes.Database, ex.Message, MessageTypes.Error).WriteLog();
                return await Task.FromResult(new List<Admin>());
            }
        }

        #region Delete Admin Async
        /// <summary>
        /// deletes the delete admin, and logs the admin who did it
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="deleteAdmin"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Admin deleteAdmin, Admin admin)
        {
            if (_repository.DeleteAsync(deleteAdmin).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} deleted admin user {deleteAdmin.UserName}", MessageTypes.Deleted).WriteLog();
                return await Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to delete admin user {deleteAdmin.UserName}", MessageTypes.Error).WriteLog();
                return await Task.FromResult(false);
            }
        }
        #endregion

        #region Update Admin Async
        /// <summary>
        /// Update admin
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="updateadmin"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Admin updateadmin, Admin admin)
        {
            if (_repository.UpdateAsync(updateadmin).Result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} updated {updateadmin.UserName}", MessageTypes.Change);
                return await Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to update admin {updateadmin.UserName}", MessageTypes.Error);
                return await Task.FromResult(false);
            }
        }
        #endregion
        #endregion
    }
}
