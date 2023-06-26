using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Contracts.Repositories;
using OnlineExamSystem.Common.Contracts.Services;
using OnlineExamSystem.Common.Dtos;
using Exceptions =OnlineExamSystem.Common.Exceptions;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Services.Services;
public class SetupService : ISetupService
{
    readonly ISetupRepository _setupRepository;
    readonly RoleManager<IdentityRole> _roleManager;
    readonly IValidator<AddRoleRequest> _addRoleValidator;
    readonly IValidator<AddUserToRoleDto> _addUserToRoleValidator;
    readonly IValidator<string> _deleteRoleValidator;

    public SetupService(ISetupRepository setupRepository,
        RoleManager<IdentityRole> roleManager,
        IValidator<AddRoleRequest> addRoleValidator,
        IValidator<AddUserToRoleDto> addUserToRoleValidator,
        IValidator<string> deleteRoleValidator)
    {
        _setupRepository = setupRepository;
        _roleManager = roleManager;
        _addRoleValidator = addRoleValidator;
        _addUserToRoleValidator = addUserToRoleValidator;
        _deleteRoleValidator = deleteRoleValidator;
    }

    public async Task<BaseResponse> CreateRole(AddRoleRequest newRole)
    {
        await validateRequestAsync(_addRoleValidator, newRole);
        var result = _setupRepository.CreateRole(newRole);
        return validateResponse(result.Result, "Failed to add new role.", "New role is added successfully.");
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
        await validateRequestAsync(_addUserToRoleValidator, addUserToRoleDto);
        var result = await _setupRepository.AddUserToRole(addUserToRoleDto);
        return validateResponse(result, "Failed to add user to this role.", "User is added to role successfully.");
    }

    public Task<BaseResponse> CreateUser(CreateUserDto createUserDto)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> DeleteRole(string id)
    {
        await validateRequestAsync(_deleteRoleValidator, id);
        var result = _setupRepository.DeleteRole(id);
        return validateResponse(result.Result, "Failed to add delete this role.", "Role is deleted successfully.");
    }


    private async Task validateRequestAsync<T>(IValidator<T> validator, T dto)
    {
        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid) throw new Exceptions.ValidationException(validationResult);
    }
    private BaseResponse validateResponse(IdentityResult result, string errorMessage, string successMessage)
    {
        if (result.Succeeded)
        {
            return new BaseResponse(successMessage, null, true);
        }
        return new BaseResponse(errorMessage, result.Errors.Select(a => a.Description).ToList(), false);
    }
}
