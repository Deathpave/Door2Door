namespace Door2DoorLib.DataModels
{
    public class Location : BaseEntity
    {
        #region Fields
        private string _name;
        private string _iconUrl;
        #endregion

        #region Properties
        public string Name { get { return _name; } }
        public string IconUrl { get { return _iconUrl; } }
        #endregion

        #region Constructor
        internal Location(string name, string iconUrl, long id = 0) : base(id)
        {
            _name = name;
            _iconUrl = iconUrl;
        }
        #endregion
    }
}
