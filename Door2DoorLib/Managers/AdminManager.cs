using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Repositories;

namespace Door2DoorLib.Managers
{
    public class AdminManager
    {
        #region Fields
        private AdminRepository _adminRepository;
        #endregion

        #region Constructor
        public AdminManager(IDatabase database)
        {
            _adminRepository = new AdminRepository(database);
        }
        #endregion

        #region Methods
        #region Check Login Async
        public Task<bool> CheckLoginAsync(Admin admin)
        {
            // TODO
            // Need encryption
            // Test encrypted data with database?
            bool result = false;
            if (result)
            {
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"Failed login with username {admin.UserName}", MessageTypes.Error);
            }

            return Task.FromResult(true);
        }
        #endregion

        #region Add Admin Async
        public Task<bool> AddAdminAsync(Admin admin, Admin newAdmin)
        {
            // TODO Replace with loggedin user
            Admin user = null;
            bool result = false;
            if (result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} created a new admin user {newAdmin.UserName}", MessageTypes.Added).WriteLog();
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to create new admin user {newAdmin.UserName}", MessageTypes.Error).WriteLog();
            }

            return Task.FromResult(true);
        }
        #endregion

        #region Delete Admin Async
        public Task<bool> DeleteAdminAsync(Admin admin, Admin deleteAdmin)
        {
            bool result = false;
            if (result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} deleted admin user {deleteAdmin.UserName}", MessageTypes.Deleted).WriteLog();
                return Task.FromResult(true);
            }
            else
            {
                LogFactory.CreateLog(LogTypes.Database, $"{admin.UserName} failed to delete admin user {deleteAdmin.UserName}", MessageTypes.Error).WriteLog();
                return Task.FromResult(false);
            }
        }
        #endregion
        #endregion
    }
}
