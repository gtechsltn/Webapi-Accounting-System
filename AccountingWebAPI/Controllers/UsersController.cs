using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // Create User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] users user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.username) || string.IsNullOrWhiteSpace(user.password_hash))
            {
                return BadRequest("Username and password are required.");
            }

            try
            {
                var userId = await _usersRepository.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetUserById), new { id = userId }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get User by ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _usersRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _usersRepository.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update User
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] users user)
        {
            if (user == null || id != user.user_id || string.IsNullOrWhiteSpace(user.username) || string.IsNullOrWhiteSpace(user.password_hash))
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                var rowsAffected = await _usersRepository.UpdateUserAsync(user);
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

        // Delete User
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var rowsAffected = await _usersRepository.DeleteUserAsync(id);
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
