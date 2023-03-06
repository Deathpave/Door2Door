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
            // Need encryption
            // Test encrypted data with database?
            bool result = false;
            if (result)
            {

            }
            else
            {

                LogFactory.CreateLog(LogTypes.Database, $"Failed login with username {admin.UserName}", MessageType.Error);
            }

            return Task.FromResult(true);
        }
        #endregion

        #region Add Admin Async
        public Task<bool> AddAdminAsync(Admin admin)
        {
            // TODO Replace with loggedin user
            Admin user = null;
            bool result = false;
            if (result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{user.UserName} created a new admin user {admin.UserName}", MessageType.Added);
            }
            else
            {
                // TODO Due to?
                LogFactory.CreateLog(LogTypes.Database, $"{user.UserName} failed to create new admin user {admin.UserName}", MessageType.Error);
            }

            return Task.FromResult(true);
        }
        #endregion

        #region Delete Admin Async
        public Task<bool> DeleteAdminAsync(Admin admin)
        {
            bool result = false;
            // TODO Replace with loggedin user
            Admin user = null;
            if (result)
            {
                LogFactory.CreateLog(LogTypes.Database, $"{user.UserName} deleted admin user {admin.UserName}", MessageType.Deleted);
            }
            else
            {
                // TODO Due to?
                LogFactory.CreateLog(LogTypes.Database, $"{user.UserName} failed to delete admin user {admin.UserName}", MessageType.Error);
            }
            return Task.FromResult(true);
        }
        #endregion
        #endregion
    }
}
