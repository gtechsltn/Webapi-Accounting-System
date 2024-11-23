using AccountingWebAPI.Data;
using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountingWebAPI.Repositorys
{
    public class AuthsRepository : IAuthsRepository
    {
        private readonly DapperContext _dbcontext;
        private readonly IConfiguration _configuration;

        public AuthsRepository(DapperContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext;
            _configuration = configuration;
        }

        // login
        public async Task<auths> GetLoginAsync(users user)
        {
            var auths = new auths();
            var query = "SELECT * FROM users WHERE email = @email AND password_hash=@password_hash;";

            var query1 = @"
            SELECT r.* 
            FROM roles r
            JOIN user_roles ur ON r.role_id = ur.role_id
            WHERE ur.user_id = @user_id;";

            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                auths = await _dbConnection.QueryFirstAsync<auths>(query, user);

                var result = await _dbConnection.QueryAsync<user_roles>(query1, auths);

                auths.roles = result.ToList();
            }

            return auths;
        }


        public async Task<string> GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<auths> ChangePassAsync(users user)
        {
            var auth = new auths();
            var query = "UPDATE users SET password_hash = @PasswordHash WHERE user_id = @UserId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                var result=await _dbConnection.ExecuteAsync(query, new { user.password_hash, UserId = user.user_id });
                if (result > 0)
                {
                    auth.user_id = user.user_id;
                }
            }

            return auth;
        }
    }
}
