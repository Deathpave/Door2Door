namespace Door2DoorLib.DataModels
{
    public abstract class BaseEntity
    {
        #region Fields
        private long _id;
        #endregion

        #region Properties
        public long Id { get { return _id; } }
        #endregion

        #region Constructor
        public BaseEntity(long id)
        {
            _id = id;
        }
        #endregion
    }
}
