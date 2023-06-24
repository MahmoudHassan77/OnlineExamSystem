using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Contracts.Repositories;
using OnlineExamSystem.Common.Contracts.Services;
using OnlineExamSystem.Common.Dtos;
using Exceptions =OnlineExamSystem.Common.Exceptions;
using OnlineExamSystem.Common.Validators;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Services.Services;
public class SetupService : ISetupService
{
    readonly ISetupRepository _setupRepository;
    readonly RoleManager<IdentityRole> _roleManager;
    readonly IValidator<AddRoleRequest> _addRoleValidator;
    readonly IValidator<AddUserToRoleDto> _addUserToRoleValidator;

    public SetupService(ISetupRepository setupRepository,
        RoleManager<IdentityRole> roleManager,
        IValidator<AddRoleRequest> addRoleValidator,
        IValidator<AddUserToRoleDto> addUserToRoleValidator)
    {
        _setupRepository = setupRepository;
        _roleManager = roleManager;
        _addRoleValidator = addRoleValidator;
        _addUserToRoleValidator = addUserToRoleValidator;
    }

    public async Task<BaseResponse> CreateRole(AddRoleRequest newRole)
    {
        var validationResult = await _addRoleValidator.ValidateAsync(newRole);
        if (!validationResult.IsValid) throw new Exceptions.ValidationException(validationResult);

        var result = _setupRepository.CreateRole(newRole);
        if (result.Result.Succeeded)
        {
            return new BaseResponse( "New role is added successfully.", null, true);
        }
        return new BaseResponse("Failed to add new role.", result.Result.Errors.Select(a => a.Description).ToList(), false);
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
        var validationResult = await _addUserToRoleValidator.ValidateAsync(addUserToRoleDto);
        if (!validationResult.IsValid) throw new Exceptions.ValidationException(validationResult);

        var result = await _setupRepository.AddUserToRole(addUserToRoleDto);
        if (result.Succeeded)
        {
            return new BaseResponse("User is added to role successfully.", null, true);
        }
        return new BaseResponse("Failed to add user to this role.", result.Errors.Select(a => a.Description).ToList(), false);
    }

}
