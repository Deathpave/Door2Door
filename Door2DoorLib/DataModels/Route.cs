namespace Door2DoorLib.DataModels
{
    public class Route : BaseEntity
    {
        #region Fields
        private long _videoId;
        private string _description;
        #endregion

        #region Properties
        public long VideoId { get { return _videoId; } }
        public string Description { get { return _description; } }
        #endregion

        #region Constructor
        public Route(long id, long videoId, string description) : base(id)
        {
            _videoId = videoId;
            _description = description;
        }
        #endregion
    }
}
