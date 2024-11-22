using AccountingWebAPI.Data;
using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using Dapper;
using System.Data;

namespace AccountingWebAPI.Repositorys
{
    public class RolesRepository : IRolesRepository
    {
        private readonly DapperContext _dbcontext;

        public RolesRepository(DapperContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Create
        public async Task<int> CreateRoleAsync(roles role)
        {
            var query = "INSERT INTO roles (role_name) VALUES (@RoleName) RETURNING role_id;";

            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteScalarAsync<int>(query, new { RoleName = role.role_name });
            }
        }

        // Read
        public async Task<roles> GetRoleByIdAsync(int roleId)
        {
            var query = "SELECT * FROM roles WHERE role_id = @RoleId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<roles>(query, new { RoleId = roleId });
            }
        }

        // Read All
        public async Task<IEnumerable<roles>> GetAllRolesAsync()
        {
            var query = "SELECT * FROM roles;";

            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.QueryAsync<roles>(query);
            }
        }

        // Update
        public async Task<int> UpdateRoleAsync(roles role)
        {
            var query = "UPDATE roles SET role_name = @RoleName WHERE role_id = @RoleId;";

            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteAsync(query, new { RoleName = role.role_name, RoleId = role.role_id });
            }
        }

        // Delete
        public async Task<int> DeleteRoleAsync(int roleId)
        {
            var query = "DELETE FROM roles WHERE role_id = @RoleId;";

            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteAsync(query, new { RoleId = roleId });
            }
        }
    }
}
