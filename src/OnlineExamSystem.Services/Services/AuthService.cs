using OnlineExamSystem.Common.Contracts.Repositories;
using OnlineExamSystem.Common.Contracts.Services;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Common.Helpers;

namespace OnlineExamSystem.Services.Services;
public class AuthService : IAuthService
{
    readonly IValidationService _validationService;
    readonly ISetupRepository _setupRepository;
    readonly IAuthRepository _authRepository;

    public AuthService(IValidationService validationService, ISetupRepository setupRepository, IAuthRepository authRepository)
    {
        _validationService = validationService;
        _setupRepository = setupRepository;
        _authRepository = authRepository;
    }
    public async Task<AuthResponse> Login(LoginDto cred)
    {
        await _validationService.EnsureValid(cred);
        var result = await _authRepository.Login(cred);
        return result;
    }

    public Task<BaseResponse> Logout()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> RefreshToken()
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> Register(CreateUserDto user, string role)
    {
        await _validationService.EnsureValid(user);

        var result = await _setupRepository.CreateUser(user, role);
        return HelperMethods.validateResponse(result, "Failed to add new user.", "User is added successfully.");

    }
}
