using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JwtApp.Core.DTOs;
using JwtApp.Core.Utilities;
using JwtApp.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JwtApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null) return Result<UserDto>.Failure("Invalid email");

            if (!user.EmailConfirmed) return Result<UserDto>.Failure("Email not confirmed");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                await SetRefreshToken(user);
                return Result<UserDto>.Success(await CreateUserDtoObject(user));
            }

            return Result<UserDto>.Failure("Invalid password");
        }

        public async Task<Result<UserDto>> RefreshToken()
        {
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            var username = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

            var user = await _userManager.Users
                .Include(r => r.RefreshTokens)
                .FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null) return Result<UserDto>.Failure("Unauthorized");

            var oldToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

            if (oldToken != null && !oldToken.IsActive) return Result<UserDto>.Failure("Unauthorized");

            if (oldToken != null) oldToken.Revoked = DateTime.UtcNow;

            return Result<UserDto>.Success(await CreateUserDtoObject(user));
        }


        private async Task SetRefreshToken(AppUser user)
        {
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            _httpContextAccessor.HttpContext?.Response.Cookies
                .Append("refreshToken", refreshToken.Token, cookieOptions);
        }

        private async Task<UserDto> CreateUserDtoObject(AppUser user)
        {
            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user)
            };
        }
    }

    public interface IAccountService
    {
        Task<Result<UserDto>> Login(LoginDto loginDto);
        Task<Result<UserDto>> RefreshToken();

    }
}