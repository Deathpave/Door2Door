using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    internal interface IRepository<T> where T : BaseEntity
    {
        #region Methods
        Task<bool> CreateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> GetByIdAsync(long id);
        Task<bool> GetAllAsync();
        Task<bool> UpdateAsync(T entity);
        #endregion
    }
}
