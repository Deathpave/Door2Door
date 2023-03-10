using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    internal interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Generic create CRUD operation
        /// </summary>
        /// <param name="createEntity"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(T createEntity);
        
        /// <summary>
        /// Generic Delete CRUD operation
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T deleteEntity);

        /// <summary>
        /// Generic Read CRUD Operation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(long id);

        /// <summary>
        /// Generic ReadAll CRUD operation
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Generic Update Crud operation
        /// </summary>
        /// <param name="updateEntity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T updateEntity);
    }
}
