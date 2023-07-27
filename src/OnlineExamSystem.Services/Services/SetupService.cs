using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Contracts.Repositories;
using OnlineExamSystem.Common.Contracts.Services;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Common.Helpers;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Services.Services;
public class SetupService : ISetupService
{
    readonly ISetupRepository _setupRepository;
    readonly IValidationService _validationService;

    public SetupService(ISetupRepository setupRepository,
        IValidationService validationService)
    {
        _setupRepository = setupRepository;
        _validationService = validationService;
    }

    public async Task<BaseResponse> CreateRole(AddRoleRequest newRole)
    {
        await _validationService.EnsureValid(newRole);
        var result = await _setupRepository.CreateRole(newRole);
        return HelperMethods.validateResponse(result, "Failed to add new role.", "New role is added successfully.");
    }

    public async Task<IEnumerable<IdentityRole>> GetAllRoles()
    {
        return await _setupRepository.GetAllRoles();
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
    {
        return await _setupRepository.GetAllUsers();
    }
    public async Task<BaseResponse> AddUserToRole(AddUserToRoleDto addUserToRoleDto)
    {
        await _validationService.EnsureValid(addUserToRoleDto);
        var result = await _setupRepository.AddUserToRole(addUserToRoleDto);
        return HelperMethods.validateResponse(result, "Failed to add user to this role.", "User is added to role successfully.");
    }

    public async Task<BaseResponse> CreateUser(CreateUserDto createUserDto, string role)
    {
        await _validationService.EnsureValid(createUserDto);
        var result = await _setupRepository.CreateUser(createUserDto, role);
        return HelperMethods.validateResponse(result, "Failed to add new user.", "User is added successfully.");
    }

    public async Task<BaseResponse> DeleteRole(string id)
    {
        await _validationService.EnsureValid(id);
        var result = await _setupRepository.DeleteRole(id);
        return HelperMethods.validateResponse(result, "Failed to delete this role.", "Role is deleted successfully.");
    }
    public async Task<BaseResponse> DeleteUserFromRole(DeleteUserfromRoleDto deleteUserfromRole)
    {
        await _validationService.EnsureValid(deleteUserfromRole);
        var result =await _setupRepository.DeleteUserFromRole(deleteUserfromRole);
        return HelperMethods.validateResponse(result, "Failed to delete user from this role.", "Role is deleted successfully from this user.");
    }
    public async Task<IEnumerable<string>> GetUserRoles(GetUserRolesDto getUserRolesDto)
    {
        await _validationService.EnsureValid(getUserRolesDto);
        return await _setupRepository.GetUserRoles(getUserRolesDto);

    }
    public async Task<BaseResponse> DeleteUser(DeleteUserDto deleteUserDto)
    {
        await _validationService.EnsureValid(deleteUserDto);
        var result = await _setupRepository.DeleteUser(deleteUserDto.Id);
        return HelperMethods.validateResponse(result, "Failed to delete this user.", "User is deleted successfully.");
    }

    
    
}
