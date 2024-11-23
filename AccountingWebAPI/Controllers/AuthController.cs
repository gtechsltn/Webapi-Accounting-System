using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using AccountingWebAPI.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AccountingWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthsRepository _authsRepository;
        private readonly IUsersRepository _usersRepository;
        public AuthController(IAuthsRepository authsRepository, IUsersRepository usersRepository)
        {
            _authsRepository = authsRepository;
            _usersRepository = usersRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] usersrequest request)
        {
            var user = new users();
            user.email = request.email;
            user.password_hash = request.password;

            var auths = await _authsRepository.GetLoginAsync(user);

            if (auths.user_id>0) // Replace with your user validation logic
            {
                var token = await _authsRepository.GenerateJwtToken(auths.username);
                return Ok(new { 
                    Token = token,
                    Username=user.username
                });
            }
            return Unauthorized();
        }


        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] users user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.username) || string.IsNullOrWhiteSpace(user.password_hash))
            {
                return BadRequest("Username and password are required.");
            }

            try
            {
                var userId = await _usersRepository.CreateUserAsync(user);

                var userinfo = await _usersRepository.GetUserByIdAsync(userId);

                return Ok(userinfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> ChangePass([FromBody] passchangerequest user)
        {
            if (string.IsNullOrWhiteSpace(user.password)!=string.IsNullOrWhiteSpace(user.confirm_password))
            {
                return BadRequest("password not match.");
            }

            try
            {
                var user_auth = new users();
                user_auth.email = user.email;
                user_auth.password_hash = user.old_password;

                var verify_auth = await _authsRepository.GetLoginAsync(user_auth);
                if (verify_auth.user_id > 0)
                {
                    user_auth.password_hash = user.password;
                    await _authsRepository.ChangePassAsync(user_auth);
                }

                return Ok(verify_auth);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
