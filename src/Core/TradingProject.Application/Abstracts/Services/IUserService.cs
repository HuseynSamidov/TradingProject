using TradingProject.Application.DTOs.UserDTOs;
using TradingProject.Application.Shared;
using TradingProject.Application.Shared.Settings;

namespace TradingProject.Application.Abstracts.Services;

public interface IUserService
{
    Task<BaseResponse<TokenResponse>> Register(UserRegisterDto dto);
    Task<BaseResponse<TokenResponse>> Login(UserLoginDto dto);
    Task<BaseResponse<TokenResponse>> ResetPassword(UserResetPasswordDto password);//UserResetPasswordDto duzeldersen



}
//AddRole
//RefreshTokenAsync
//ConfirmEmail
//ConfirmAsync