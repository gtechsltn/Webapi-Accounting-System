using AccountingWebAPI.Data;
using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using Dapper;
using System.Data;

namespace AccountingWebAPI.Repositorys
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly DapperContext _dbcontext;

        public UserRolesRepository(DapperContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Create
        public async Task<int> AssignRoleToUserAsync(user_roles userRole)
        {
            var query = "INSERT INTO user_roles (user_id, role_id) VALUES (@UserId, @RoleId);";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteAsync(query, new { UserId = userRole.user_id, RoleId = userRole.role_id });
            }
        }

        // Read All roles for a user
        public async Task<IEnumerable<roles>> GetRolesByUserIdAsync(int userId)
        {
            var query = @"
            SELECT r.* 
            FROM roles r
            JOIN user_roles ur ON r.role_id = ur.role_id
            WHERE ur.user_id = @UserId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {

                return await _dbConnection.QueryAsync<roles>(query, new { UserId = userId });
            }
        }

        // Read All users with a specific role
        public async Task<IEnumerable<users>> GetUsersByRoleIdAsync(int roleId)
        {
            var query = @"
            SELECT u.* 
            FROM users u
            JOIN user_roles ur ON u.user_id = ur.user_id
            WHERE ur.role_id = @RoleId;";

            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.QueryAsync<users>(query, new { RoleId = roleId });
            }
        }

        // Delete role from user
        public async Task<int> RemoveRoleFromUserAsync(user_roles userRole)
        {
            var query = "DELETE FROM user_roles WHERE user_id = @UserId AND role_id = @RoleId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteAsync(query, new { UserId = userRole.user_id, RoleId = userRole.role_id });
            }
        }

        // Delete all roles from a user
        public async Task<int> RemoveAllRolesFromUserAsync(int userId)
        {
            var query = "DELETE FROM user_roles WHERE user_id = @UserId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteAsync(query, new { UserId = userId });
            }
        }

        // Delete all users from a role
        public async Task<int> RemoveAllUsersFromRoleAsync(int roleId)
        {
            var query = "DELETE FROM user_roles WHERE role_id = @RoleId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteAsync(query, new { RoleId = roleId });
            }
        }
    }
}
