﻿using Door2DoorLib.DataModels;

namespace Door2DoorLib.Factories
{
    /// <summary>
    /// Factory that handles the creation of Location objects
    /// </summary>
    public class LocationFactory
    {
        #region Methods
        #region Create Location
        public static Location CreateLocation(string name, string iconurl = "", long id = 0)
        {
            return new Location(name, iconurl, id);
        }
        #endregion
        #endregion
    }
}
