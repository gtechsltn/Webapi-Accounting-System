using AccountingWebAPI.Interfaces;
using AccountingWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class VouchersController : ControllerBase
    {
        private readonly IVouchersRepository _vouchersRepository;

        public VouchersController(IVouchersRepository vouchersRepository)
        {
            _vouchersRepository = vouchersRepository;
        }

        // Create Voucher
        [HttpPost]
        public async Task<IActionResult> CreateVoucher([FromBody] vouchers voucher)
        {
            if (voucher == null)
            {
                return BadRequest("Voucher data is null.");
            }

            try
            {
                var voucherId = await _vouchersRepository.CreateVoucherAsync(voucher);
                return Ok(new { Message = "Voucher created successfully.", VoucherId = voucherId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get Voucher by ID
        [HttpGet("{voucherId:int}")]
        public async Task<IActionResult> GetVoucherById(int voucherId)
        {
            try
            {
                var voucher = await _vouchersRepository.GetVoucherByIdAsync(voucherId);
                if (voucher == null)
                {
                    return NotFound("Voucher not found.");
                }
                return Ok(voucher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get All Vouchers
        [HttpGet]
        public async Task<IActionResult> GetAllVouchers()
        {
            try
            {
                var vouchers = await _vouchersRepository.GetAllVouchersAsync();
                return Ok(vouchers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update Voucher
        [HttpPut("{voucherId:int}")]
        public async Task<IActionResult> UpdateVoucher(int voucherId, [FromBody] vouchers voucher)
        {
            if (voucher == null || voucher.voucher_id != voucherId)
            {
                return BadRequest("Voucher data is invalid.");
            }

            try
            {
                var rowsAffected = await _vouchersRepository.UpdateVoucherAsync(voucher);
                if (rowsAffected > 0)
                {
                    return Ok("Voucher updated successfully.");
                }
                return NotFound("Voucher not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete Voucher
        [HttpDelete("{voucherId:int}")]
        public async Task<IActionResult> DeleteVoucher(int voucherId)
        {
            try
            {
                var rowsAffected = await _vouchersRepository.DeleteVoucherAsync(voucherId);
                if (rowsAffected > 0)
                {
                    return Ok("Voucher deleted successfully.");
                }
                return NotFound("Voucher not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
