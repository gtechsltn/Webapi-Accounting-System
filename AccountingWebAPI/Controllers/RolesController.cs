using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        // Create Role
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] roles role)
        {
            if (role == null || string.IsNullOrWhiteSpace(role.role_name))
            {
                return BadRequest("Role name is required.");
            }

            try
            {
                var roleId = await _rolesRepository.CreateRoleAsync(role);
                return CreatedAtAction(nameof(GetRoleById), new { id = roleId }, role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get Role by ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var role = await _rolesRepository.GetRoleByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }

                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get All Roles
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _rolesRepository.GetAllRolesAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update Role
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] roles role)
        {
            if (role == null || id != role.role_id || string.IsNullOrWhiteSpace(role.role_name))
            {
                return BadRequest("Invalid role data.");
            }

            try
            {
                var rowsAffected = await _rolesRepository.UpdateRoleAsync(role);
                if (rowsAffected == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete Role
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var rowsAffected = await _rolesRepository.DeleteRoleAsync(id);
                if (rowsAffected == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
