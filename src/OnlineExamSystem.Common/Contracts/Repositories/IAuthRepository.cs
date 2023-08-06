using OnlineExamSystem.Common.Dtos;

namespace OnlineExamSystem.Common.Contracts.Repositories;
public interface IAuthRepository
{
    Task<AuthResponse> Login(LoginDto cred);
    Task<AuthResponse> Register(CreateUserDto user, string role);
}
