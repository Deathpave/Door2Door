namespace Door2DoorLib.DataModels
{
    public class Route : BaseEntity
    {
        #region Fields
        private string _videoUrl;
        private string _description;
        private string _name;
        #endregion

        #region Properties
        public string VideoUrl { get { return _videoUrl; } }
        public string Description { get { return _description; } }
        public string Name { get { return _name; } }
        #endregion

        #region Constructor
        public Route(long id, string videoUrl, string description, string name) : base(id)
        {
            _videoUrl = videoUrl;
            _description = description;
            _name = name;
        }
        #endregion
    }
}
