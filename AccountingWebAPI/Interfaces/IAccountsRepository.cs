using AccountingWebAPI.Models;

namespace AccountingWebAPI.Interfaces
{
    public interface IAccountsRepository
    {
        Task<int> CreateAccountAsync(accounts account);
        Task<int> DeleteAccountAsync(int accountId);
        Task<accounts> GetAccountByIdAsync(int accountId);
        Task<IEnumerable<accounts>> GetAllAccountsAsync();
        Task<int> UpdateAccountAsync(accounts account);
    }
}