using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TradingProject.Application.Abstracts.Services;
using TradingProject.Application.DTOs.UserDTOs;
using TradingProject.Application.Shared;
using TradingProject.Application.Shared.Settings;
using TradingProject.Domain.Entities;

namespace TradingProject.Persistence.Services
{
    public class UserService : IUserService
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> SignInManager { get; }
        private JWTSettings _jwtSettings { get; }

        public UserService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            SignInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }



        public async Task<BaseResponse<TokenResponse>> Register(UserRegisterDto dto)
        {
            var existEmail = await _userManager.FindByEmailAsync(dto.Email);

            if (existEmail is not null)
            {
                return new BaseResponse<TokenResponse>("This account already exist", HttpStatusCode.BadRequest);
            }

            AppUser newUser = new()
            {
                Email = dto.Email,
                FullName = dto.FullName,
                UserName = dto.Email
            };

            IdentityResult identityResult = await _userManager.CreateAsync(newUser, dto.Password);

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors;
                StringBuilder errorMessage = new StringBuilder();
                foreach (var error in errors)
                {
                    errorMessage.Append(error.Description + ";");
                }
                return new(errorMessage.ToString(), HttpStatusCode.BadRequest);
            }
            return new("Succesfully created", HttpStatusCode.Created);
        }

        public async Task<BaseResponse<TokenResponse>> Login(UserLoginDto dto)
        {
            var existUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existUser is null)
            {
                return new("Email or password is wrong", null, HttpStatusCode.NotFound);
            }



            SignInResult signInResult = await SignInManager.PasswordSignInAsync
                (dto.Email, dto.Password, true, true);


            if (!signInResult.Succeeded)
            {
                return new("Email or password is wrong", null, HttpStatusCode.NotFound);
            }

            var token = GenerateJwtToken(dto.Email);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);
            TokenResponse tokenResponse = new()
            {
                Token = token,
                ExpireDate = expires
            };
            return new("Token generated", tokenResponse, HttpStatusCode.OK);
        }
        private string GenerateJwtToken(string userEmail)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Email, userEmail),
        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }

}
