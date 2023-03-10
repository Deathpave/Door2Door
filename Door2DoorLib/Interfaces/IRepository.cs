using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    internal interface IRepository<T> where T : BaseEntity
    {
        #region Methods
        Task<bool> CreateAsync(T createEntity);
        Task<bool> DeleteAsync(T deleteEntity);
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> UpdateAsync(T updateEntity);
        #endregion
    }
}
