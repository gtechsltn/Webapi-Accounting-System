using AccountingWebAPI.Data;
using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace AccountingWebAPI.Repositorys
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperContext _dbcontext;

        public UsersRepository(DapperContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Create
        public async Task<int> CreateUserAsync(users user)
        {
            var query = "INSERT INTO users (username, password_hash) VALUES (@Username, @PasswordHash) RETURNING user_id;";


            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteScalarAsync<int>(query, new { user.username, PasswordHash=user.password_hash });
            }
        }

        // Read
        public async Task<users> GetUserByIdAsync(int userId)
        {
            var query = "SELECT * FROM users WHERE user_id = @UserId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<users>(query, new { UserId = userId });
            }
        }

        // Read All
        public async Task<IEnumerable<users>> GetAllUsersAsync()
        {
            var query = "SELECT * FROM users;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.QueryAsync<users>(query);
            }
        }

        // Update
        public async Task<int> UpdateUserAsync(users user)
        {
            var query = "UPDATE users SET username = @Username, password_hash = @PasswordHash WHERE user_id = @UserId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteAsync(query, new { user.username, user.password_hash, UserId = user.user_id });
            }
        }

        // Delete
        public async Task<int> DeleteUserAsync(int userId)
        {
            var query = "DELETE FROM users WHERE user_id = @UserId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteAsync(query, new { UserId = userId });
            }
        }
    }
}
