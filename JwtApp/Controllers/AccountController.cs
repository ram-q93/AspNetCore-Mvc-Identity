using System.Threading.Tasks;
using JwtApp.Core.DTOs;
using JwtApp.Core.Utilities;
using JwtApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<Result<UserDto>> Login(LoginDto loginDto)
        {
            return await _accountService.Login(loginDto);
        }

        [Authorize]
        [HttpPost("refreshToken")]
        public async Task<Result<UserDto>> RefreshToken()
        {
            return await _accountService.RefreshToken();
        }


    }
}