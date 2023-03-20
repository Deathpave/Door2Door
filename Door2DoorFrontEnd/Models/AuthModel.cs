namespace Door2DoorFrontEnd.Models
{
    public class AuthModel
    {
        #region Properties
        public string Username { get; set; }

        public string Password { get; set; }

        public int Authenticated { get; set; }
        #endregion

        #region Constructor
        public AuthModel(string userName, int authenticated)
        {
            Username = userName;
            Authenticated = authenticated;
        }
        #endregion
    }
}
