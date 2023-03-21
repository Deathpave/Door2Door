namespace Door2DoorFrontEnd.Models
{
    public class AdminModel
    {
        public AuthModel Auth { get; set; }
        public string? Username { get; set; }
        public string? NewAdminUsername { get; set; }
        public string? NewAdminPswd { get; set; }
        public string? DeleteAdmin { get; set; }
        public FileModel? File { get; set; }
        public string? NewLocationName { get; set; }
        public RouteModel? RouteModel { get; set; }
        public AdminModel()
        {
            Auth = new AuthModel();
            File = new FileModel();
            RouteModel = new RouteModel();
        }
    }
}
