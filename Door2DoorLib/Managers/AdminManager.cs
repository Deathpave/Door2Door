using Door2DoorLib.Adapters;
using Door2DoorLib.DataModels;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return Task.FromResult(true);
        }
        #endregion

        #region Add Admin Async
        public Task<bool> AddAdminAsync(Admin admin)
        {
            return Task.FromResult(true);
        }
        #endregion

        #region Delete Admin Async
        public Task<bool> DeleteAdminAsync(Admin admin)
        {
            return Task.FromResult(true);
        }
        #endregion
        #endregion
    }
}
