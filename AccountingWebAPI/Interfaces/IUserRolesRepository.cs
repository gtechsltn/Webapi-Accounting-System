using AccountingWebAPI.Models;

namespace AccountingWebAPI.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<int> AssignRoleToUserAsync(user_roles userRole);
        Task<IEnumerable<roles>> GetRolesByUserIdAsync(int userId);
        Task<IEnumerable<users>> GetUsersByRoleIdAsync(int roleId);
        Task<int> RemoveAllRolesFromUserAsync(int userId);
        Task<int> RemoveAllUsersFromRoleAsync(int roleId);
        Task<int> RemoveRoleFromUserAsync(user_roles userRole);
    }
}