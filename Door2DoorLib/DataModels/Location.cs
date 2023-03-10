﻿namespace Door2DoorLib.DataModels
{
    public class Location
    {
        #region Fields
        private long _id;
        private string _name;
        private string _iconUrl;
        #endregion

        #region Properties
        public long Id { get { return _id; } }
        public string Name { get { return _name; } }
        public string IconUrl { get { return _iconUrl; } }
        #endregion

        #region Constructor
        public Location(long id, string name, string iconUrl)
        {
            _id = id;
            _name = name;
            _iconUrl = iconUrl;
        }
        #endregion
    }
}