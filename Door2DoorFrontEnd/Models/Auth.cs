namespace Door2DoorFrontEnd.Models
{
    public class Auth
    {
        #region Properties
        public string Username { get; set; }

        public string Password { get; set; }

        public int Authenticated { get; set; }
        #endregion

        #region Constructor
        public Auth(string userName, int authenticated)
        {
            Username = userName;
            Authenticated = authenticated;
        }
        #endregion
    }
}
