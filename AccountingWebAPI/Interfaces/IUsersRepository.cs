using AccountingWebAPI.Models;

namespace AccountingWebAPI.Interfaces
{
    public interface IUsersRepository
    {
        Task<int> CreateUserAsync(users user);
        Task<int> DeleteUserAsync(int userId);
        Task<IEnumerable<users>> GetAllUsersAsync();
        Task<users> GetUserByIdAsync(int userId);
        Task<int> UpdateUserAsync(users user);
    }
}