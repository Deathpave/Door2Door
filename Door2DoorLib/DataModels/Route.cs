namespace Door2DoorLib.DataModels
{
    /// <summary>
    /// Object class for Route entities
    /// </summary>
    public class Route : BaseEntity
    {
        #region Fields
        private string _videoUrl;
        private string _description;
        private long _startLocationId;
        private long _endLocationId;
        #endregion

        #region Properties
        public string VideoUrl { get { return _videoUrl; } }
        public string Description { get { return _description; } }
        public long StartLocation { get { return _startLocationId; } }
        public long EndLocation { get { return _endLocationId; } }
        #endregion

        #region Constructor
        internal Route(string videoUrl, string description, long startLocationId, long endLocationId, long id = 0) : base(id)
        {
            _videoUrl = videoUrl;
            _description = description;
            _startLocationId = startLocationId;
            _endLocationId = endLocationId;
        }
        #endregion
    }
}
