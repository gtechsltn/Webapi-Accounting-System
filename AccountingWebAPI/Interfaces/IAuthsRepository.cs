using AccountingWebAPI.Models;

namespace AccountingWebAPI.Interfaces
{
    public interface IAuthsRepository
    {
        Task<string> GenerateJwtToken(string username, List<user_roles> rolelist);
        Task<auths> GetLoginAsync(users user);

        Task<auths> ChangePassAsync(users user);
    }
}