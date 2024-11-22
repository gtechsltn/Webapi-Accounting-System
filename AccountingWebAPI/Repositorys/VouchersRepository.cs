using AccountingWebAPI.Data;
using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using Dapper;
using System.Data;

namespace AccountingWebAPI.Repositorys
{
    public class VouchersRepository : IVouchersRepository
    {
        private readonly DapperContext _dbcontext;

        public VouchersRepository(DapperContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Create
        public async Task<int> CreateVoucherAsync(vouchers voucher)
        {
            var query = @"
            INSERT INTO vouchers (voucher_date, description, total_amount) 
            VALUES (@VoucherDate, @Description, @TotalAmount) 
            RETURNING voucher_id;";

            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteScalarAsync<int>(query,
                new { VoucherDate = voucher.voucher_date, Description = voucher.description, TotalAmount = voucher.total_amount });
            }
        }

        // Read (Get Voucher by ID)
        public async Task<vouchers> GetVoucherByIdAsync(int voucherId)
        {
            var query = "SELECT * FROM vouchers WHERE voucher_id = @VoucherId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<vouchers>(query, new { VoucherId = voucherId });
            }
        }

        // Read All
        public async Task<IEnumerable<vouchers>> GetAllVouchersAsync()
        {
            var query = "SELECT * FROM vouchers;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.QueryAsync<vouchers>(query);
            }
        }

        // Update
        public async Task<int> UpdateVoucherAsync(vouchers voucher)
        {
            var query = @"
            UPDATE vouchers 
            SET voucher_date = @VoucherDate, 
                description = @Description, 
                total_amount = @TotalAmount 
            WHERE voucher_id = @VoucherId;";

            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {

                return await _dbConnection.ExecuteAsync(query,
                new { VoucherDate = voucher.voucher_date, Description = voucher.description, TotalAmount = voucher.total_amount, VoucherId = voucher.voucher_id });
            }
        }

        // Delete
        public async Task<int> DeleteVoucherAsync(int voucherId)
        {
            var query = "DELETE FROM vouchers WHERE voucher_id = @VoucherId;";
            using (var _dbConnection = _dbcontext.CreateDbConnection())
            {
                return await _dbConnection.ExecuteAsync(query, new { VoucherId = voucherId });
            }
        }
    }
}
