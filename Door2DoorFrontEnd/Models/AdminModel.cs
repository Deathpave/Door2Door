﻿using Door2DoorLib.DataModels;

namespace Door2DoorFrontEnd.Models
{
    public class AdminModel
    {
        public string Username { get; set; }

        public Admin newadmin { get; set; }

        public Door2DoorLib.DataModels.Route newroute { get; set; }

        public string FilePath { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }


    }

}
