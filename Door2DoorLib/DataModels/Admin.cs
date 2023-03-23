namespace Door2DoorLib.DataModels
{
    /// <summary>
    /// Object class for Admin users
    /// </summary>
    public class Admin : BaseEntity
    {
        #region Fields
        private string _username;
        private string _password;
        #endregion

        #region Properties
        public string UserName { get { return _username; } }
        public string Password { get { return _password; } }
        #endregion

        #region Constructor
        internal Admin(string username, string password, long id = 0) : base(id)
        {
            _username = username;
            _password = password;
        }
        #endregion
    }
}
