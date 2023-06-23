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
    readonly IValidator<AddRoleRequest> _validator;

    public SetupService(ISetupRepository setupRepository, RoleManager<IdentityRole> roleManager, IValidator<AddRoleRequest> validator)
    {
        _setupRepository = setupRepository;
        _roleManager = roleManager;
        _validator = validator;
    }

    public async Task<BaseResponse> CreateRole(AddRoleRequest newRole)
    {
        var validationResult = await _validator.ValidateAsync(newRole);
        if (!validationResult.IsValid) throw new Exceptions.ValidationException(validationResult);

        var result = _setupRepository.CreateRole(newRole);
        if (result.Result.Succeeded)
        {
            return new BaseResponse(Guid.NewGuid().ToString(), "New role is added successfully.", null, true);
        }
        return new BaseResponse(Guid.NewGuid().ToString(), "Failed to add new role.", result.Result.Errors.Select(a => a.Description).ToList(), false);
    }

    public async Task<IEnumerable<IdentityRole>> GetAllRoles()
    {
        return await _setupRepository.GetAllRoles();
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
    {
        return await _setupRepository.GetAllUsers();
    }
}
