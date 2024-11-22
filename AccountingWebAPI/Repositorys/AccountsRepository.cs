using AccountingWebAPI.Data;
using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using Dapper;
using System.Data;

namespace AccountingWebAPI.Repositorys
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly DapperContext _dbcontext;

        public AccountsRepository(DapperContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Create
        public async Task<int> CreateAccountAsync(accounts account)
        {
            var query = @"
            INSERT INTO accounts (account_number, account_name, account_type, balance) 
            VALUES (@AccountNumber, @AccountName, @AccountType, @Balance) 
            RETURNING account_id;";

            using (var _dbConnection = _dbcontext.CreateDbConnection()) { 

            return await _dbConnection.ExecuteScalarAsync<int>(query,
                new { account.account_number, account.account_name, account.account_type, account.balance });
            }
        }

        // Read (Get Account by ID)
        public async Task<accounts> GetAccountByIdAsync(int accountId)
        {
            var query = "SELECT * FROM accounts WHERE account_id = @AccountId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<accounts>(query, new { AccountId = accountId });
            }
        }

        // Read All
        public async Task<IEnumerable<accounts>> GetAllAccountsAsync()
        {
            var query = "SELECT * FROM accounts;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.QueryAsync<accounts>(query);
            }
        }

        // Update
        public async Task<int> UpdateAccountAsync(accounts account)
        {
            var query = @"
            UPDATE accounts 
            SET account_number = @AccountNumber, 
                account_name = @AccountName, 
                account_type = @AccountType, 
                balance = @Balance 
            WHERE account_id = @AccountId;";

            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {

                return await _dbConnection.ExecuteAsync(query,
                new { account.account_number, account.account_name, account.account_type, account.balance, account.account_id });
            }
        }

        // Delete
        public async Task<int> DeleteAccountAsync(int accountId)
        {
            var query = "DELETE FROM accounts WHERE account_id = @AccountId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteAsync(query, new { AccountId = accountId });
            }
        }
    }
}
