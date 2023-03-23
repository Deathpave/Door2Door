using Door2DoorLib.DataModels;

namespace Door2DoorLib.Factories
{
    /// <summary>
    /// Factory that handles the creation of Admin objects
    /// </summary>
    public class AdminFactory
    {
        #region Methods
        #region Create Admin
        /// <summary>
        /// Creates an Admin object
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns>Admin</returns>
        public static Admin CreateAdmin(string userName, string password = "", long id = 0)
        {
            return new Admin(userName, password, id);
        }
        #endregion
        #endregion
    }
}
