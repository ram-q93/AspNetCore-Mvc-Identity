using System.Threading.Tasks;
using JwtApp.Core.DTOs;
using JwtApp.Core.Utilities;
using JwtApp.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JwtApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
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
                return Result<UserDto>.Success(await CreateUserDtoObject(user));
            }

            return Result<UserDto>.Failure("Invalid password");
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

    }
}