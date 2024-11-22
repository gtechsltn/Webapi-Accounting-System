using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesRepository _userRolesRepository;

        public UserRolesController(IUserRolesRepository userRolesRepository)
        {
            _userRolesRepository = userRolesRepository;
        }

        // Assign Role to User
        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser([FromBody] user_roles userRole)
        {
            if (userRole == null || userRole.user_id <= 0 || userRole.role_id <= 0)
            {
                return BadRequest("Invalid user or role ID.");
            }

            try
            {
                await _userRolesRepository.AssignRoleToUserAsync(userRole);
                return Ok("Role assigned to user successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get All Roles for a User
        [HttpGet("user/{userId:int}/roles")]
        public async Task<IActionResult> GetRolesByUserId(int userId)
        {
            try
            {
                var roles = await _userRolesRepository.GetRolesByUserIdAsync(userId);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get All Users with a Specific Role
        [HttpGet("role/{roleId:int}/users")]
        public async Task<IActionResult> GetUsersByRoleId(int roleId)
        {
            try
            {
                var users = await _userRolesRepository.GetUsersByRoleIdAsync(roleId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Remove Role from User
        [HttpDelete]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] user_roles userRole)
        {
            if (userRole == null || userRole.user_id <= 0 || userRole.role_id <= 0)
            {
                return BadRequest("Invalid user or role ID.");
            }

            try
            {
                await _userRolesRepository.RemoveRoleFromUserAsync(userRole);
                return Ok("Role removed from user successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Remove All Roles from User
        [HttpDelete("user/{userId:int}/roles")]
        public async Task<IActionResult> RemoveAllRolesFromUser(int userId)
        {
            try
            {
                await _userRolesRepository.RemoveAllRolesFromUserAsync(userId);
                return Ok("All roles removed from user successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Remove All Users from Role
        [HttpDelete("role/{roleId:int}/users")]
        public async Task<IActionResult> RemoveAllUsersFromRole(int roleId)
        {
            try
            {
                await _userRolesRepository.RemoveAllUsersFromRoleAsync(roleId);
                return Ok("All users removed from role successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
