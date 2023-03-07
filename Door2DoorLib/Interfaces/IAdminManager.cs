using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    public interface IAdminManager
    {
        Task<bool> CheckLoginAsync(string username, string password);
        Task<bool> AddAdminAsync(Admin newAdmin, Admin admin);
        Task<bool> DeleteAdminAsync(Admin deleteAdmin, Admin admin);
        Task<bool> UpdateAdminAsync(Admin updateAdmin, Admin admin);
    }
}
