namespace Door2DoorLib.DataModels
{
    public class Admin : BaseEntity
    {
        #region Fields
        private string _userName;
        private string _password;
        #endregion

        #region Properties
        public string UserName { get { return _userName; } }
        public string Password { get { return _password; } }
        #endregion

        #region Constructor
        public Admin(long id, string userName, string password) : base(id)
        {
            _userName = userName;
            _password = password;
        }
        #endregion
    }
}
