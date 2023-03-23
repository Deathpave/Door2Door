namespace Door2DoorFrontEnd.Models
{
    // Model to handle authentication for admins
    public class AuthModel
    {
        #region Properties
        public string Username { get; set; }

        public string Password { get; set; }

        public int Authenticated { get; set; }
        #endregion
    }
}
