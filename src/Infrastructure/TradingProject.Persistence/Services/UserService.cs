using TradingProject.Application.Abstracts.Services;
using TradingProject.Application.DTOs.UserDTOs;
using TradingProject.Application.Shared.Settings;

namespace TradingProject.Persistence.Services
{
    public class UserService : IUserService
    {
        public Task<BaseResponse<TokenResponse>> Login(UserLoginDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<TokenResponse>> Register(UserRegisterDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
