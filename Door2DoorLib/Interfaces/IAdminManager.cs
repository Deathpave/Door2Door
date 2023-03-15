using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    public interface IAdminManager
    {
        /// <summary>
        /// Validates admin login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pswd"></param>
        /// <returns></returns>
        Task<bool> CheckLoginAsync(string userName, string password);

        /// <summary>
        /// Adds new admin, and logs the admin who did it
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="newAdmin"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(Admin newAdmin, Admin admin);

        Task<IEnumerable<Admin>> GetAllAsync();

        /// <summary>
        /// deletes the delete admin, and logs the admin who did it
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="deleteAdmin"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Admin deleteAdmin, Admin admin);

        /// <summary>
        /// Update admin
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="updateadmin"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Admin updateAdmin, Admin admin);
    }
}
