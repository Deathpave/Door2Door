﻿namespace Door2DoorFrontEnd.Models
{
    public class FileModel
    {
        public IFormFile Video { get; set; }
        public IFormFile NewLocationIcon { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }
    }
}
