using OnlineExamSystem.Common.Dtos;

namespace OnlineExamSystem.Common.Contracts.Services;
public interface IAuthService
{
    Task<BaseResponse> Register(CreateUserDto user, string role);
    Task<AuthResponse> Login(LoginDto cred);
    Task<BaseResponse> Logout();
    Task<BaseResponse> RefreshToken();
}
