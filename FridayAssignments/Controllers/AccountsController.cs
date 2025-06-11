using FridayAssignments.Controllers.Helper;
using FridayAssignments.Models.DTOs;
using FridayAssignments.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FridayAssignments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountsDTO input)
        {
            if (!ModelState.IsValid)
                return ApiResponseHelper.ApiResponse(HttpStatusCode.BadRequest, "Data tidak valid");

            var account = await _accountRepository.GetByEmailAsync(input.Email);
            if (account == null)
                return ApiResponseHelper.ApiResponse(HttpStatusCode.NotFound, "Email tidak terdaftar");

            var isPasswordValid = _accountRepository.VerifyPassword(account, input.Password);
            if (!isPasswordValid)
                return ApiResponseHelper.ApiResponse(HttpStatusCode.Unauthorized, "Password salah");

            var response = new
            {
                Email = account.Email,
                FullName = $"{account.Employee.FirstName} {account.Employee.LastName}"
                // bisa tambahkan Token jika pakai JWT
            };

            return ApiResponseHelper.ApiResponse(HttpStatusCode.OK, "Login berhasil", response);
        }
    }
}
