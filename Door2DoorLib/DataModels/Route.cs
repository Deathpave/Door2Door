namespace Door2DoorLib.DataModels
{
    public class Route : BaseEntity
    {
        #region Fields
        private long _videoId;
        private string _description;
        private string _name;
        #endregion

        #region Properties
        public long VideoId { get { return _videoId; } }
        public string Description { get { return _description; } }
        public string Name { get { return _name; } }
        #endregion

        #region Constructor
        public Route(long id, long videoId, string description, string name) : base(id)
        {
            _videoId = videoId;
            _description = description;
            _name = name;
        }
        #endregion
    }
}
