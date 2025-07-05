using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        //public async Task<bool> ResetPasswordAsync(string username, string newPassword)
        //{
        //    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Username == username);
        //    if (user == null)
        //        return false;

        //    // Parolu hash-lamaq tövsiyə olunur!
        //    user.Password = newPassword;

        //    _userManager.Users.Update(user);
        //    await _userManager.SaveChangesAsync();

        //    return true;
        ////}
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

        public async Task<BaseResponse<TokenResponse>> ResetPassword(UserResetPasswordDto dto)
        {
            #region Pass
            //var user = await _userManager.AppUser.FirstOrDefaultAsync(u => u.Name == Name);
            //if (user == null)
            //    return false;

            //// Parolu hash-lamaq tövsiyə olunur!
            //user.Password = newPassword;

            //_userManager.Users.Update(user);
            //await _userManager.SaveChangesAsync();

            //return true;
            #endregion
            var resetUser = await _userManager.FindByNameAsync(dto.Name);
            if (resetUser == null)
            {
                return new BaseResponse<TokenResponse>("This account does not exist", HttpStatusCode.BadRequest);
            }
            resetUser.PasswordHash = _userManager.PasswordHasher.HashPassword(resetUser, dto.Password);

            var result = await _userManager.UpdateAsync(resetUser);
            return new BaseResponse<TokenResponse>("Password changed successfully", HttpStatusCode.OK);

        }

        public async Task<BaseResponse<TokenResponse>> ConfirmEmail(UseronfirmEmailDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
            {
                return new BaseResponse<TokenResponse>("Email cant be found", HttpStatusCode.BadRequest);
            }
            var result = await _userManager.ConfirmEmailAsync(user, dto.Token);
            return new BaseResponse<TokenResponse>("Email confirmed succesfully", HttpStatusCode.OK);
        }

        public async Task<BaseResponse<List<UserProfileDto>>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = new List<UserProfileDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserProfileDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "NoRole",
                    IsActive = user.LockoutEnd == null || user.LockoutEnd <= DateTimeOffset.Now
                });
            }

            return new BaseResponse<List<UserProfileDto>>("All profiles", HttpStatusCode.OK);
        }

        public async Task<BaseResponse<UserProfileDto>> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new BaseResponse<UserProfileDto>( "User not found", HttpStatusCode.NotFound);

            var roles = await _userManager.GetRolesAsync(user);

            var dto = new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "NoRole",
                IsActive = user.LockoutEnd == null || user.LockoutEnd <= DateTimeOffset.Now
            };

            return new BaseResponse<UserProfileDto>("User profile", HttpStatusCode.OK);
        }

    }

}
