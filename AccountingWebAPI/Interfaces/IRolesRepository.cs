using AccountingWebAPI.Models;

namespace AccountingWebAPI.Interfaces
{
    public interface IRolesRepository
    {
        Task<int> CreateRoleAsync(roles role);
        Task<int> DeleteRoleAsync(int roleId);
        Task<IEnumerable<roles>> GetAllRolesAsync();
        Task<roles> GetRoleByIdAsync(int roleId);
        Task<int> UpdateRoleAsync(roles role);
    }
}