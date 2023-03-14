using Door2DoorLib.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Door2DoorFrontEnd.Models
{
    public class AdminModel
    {
        public string Username { get; set; }

        public string NewAdminUsername { get; set; }

        public string NewAdminPswd { get; set; }

        public string NewRouteStart { get; set; }

        public string NewRouteEnd { get; set; }

        public string NewRouteDescription { get; set; }

        public string NewRouteVideoUrl { get; set; }

        public string DeleteRoute { get; set; }

        public IFormFile Video { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public string NewLocationName { get; set; }

        public IFormFile NewLocationIcon { get; set; }

        public IEnumerable<Location> LocationList { get; set; }

        public int SelectedStartLocation { get; set; }
        public int SelectedEndLocation { get; set; }

    }

}
