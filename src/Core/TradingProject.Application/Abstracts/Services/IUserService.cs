using TradingProject.Application.DTOs.UserDTOs;
using TradingProject.Application.Shared;
using TradingProject.Application.Shared.Settings;

namespace TradingProject.Application.Abstracts.Services;

public interface IUserService
{
    Task<BaseResponse<TokenResponse>> Register(UserRegisterDto dto);
    Task<BaseResponse<TokenResponse>> Login(UserLoginDto dto);
    Task<BaseResponse<TokenResponse>> ResetPassword(UserResetPasswordDto dto);//UserResetPasswordDto duzeldersen
    Task<BaseResponse<TokenResponse>> ConfirmEmail(UseronfirmEmailDto dto);
    Task<BaseResponse<List<UserProfileDto>>> GetAllAsync();
    Task<BaseResponse<UserProfileDto>> GetByIdAsync(string id);
}
//AddRole
//RefreshTokenAsync
//ConfirmAsync