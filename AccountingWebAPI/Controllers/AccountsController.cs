using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsRepository _accountRepository;       

        public AccountsController(IAccountsRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<accounts>>> GetAccounts()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<accounts>> GetAccount(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null)
                return NotFound();
            return Ok(account);
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateAccount(accounts account)
        {
            var accountId = await _accountRepository.CreateAccountAsync(account);
            return CreatedAtAction(nameof(GetAccount), new { id = accountId }, accountId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, accounts account)
        {
            if (id != account.account_id)
                return BadRequest();

            await _accountRepository.UpdateAccountAsync(account);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountRepository.DeleteAccountAsync(id);
            return NoContent();
        }
    }
}
