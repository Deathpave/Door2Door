using Door2DoorLib.DataModels;
using Microsoft.AspNetCore.Http;
namespace Door2DoorFrontEnd.Models
{
    public class AdminModel
    {
        public string Username { get; set; }

        public string NewAdminUsername { get; set; }
        public string NewAdminPswd { get; set; }

        public Door2DoorLib.DataModels.Route newroute { get; set; }

        public IFormFile Video { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }


    }

}
