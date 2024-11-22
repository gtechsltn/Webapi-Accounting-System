using AccountingWebAPI.Models;

namespace AccountingWebAPI.Interfaces
{
    public interface IVouchersRepository
    {
        Task<int> CreateVoucherAsync(vouchers voucher);
        Task<int> DeleteVoucherAsync(int voucherId);
        Task<IEnumerable<vouchers>> GetAllVouchersAsync();
        Task<vouchers> GetVoucherByIdAsync(int voucherId);
        Task<int> UpdateVoucherAsync(vouchers voucher);
    }
}