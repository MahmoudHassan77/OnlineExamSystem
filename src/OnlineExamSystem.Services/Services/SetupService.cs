using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Contracts.Repositories;
using OnlineExamSystem.Common.Contracts.Services;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Common.Exceptions;
using OnlineExamSystem.Common.Validators;

namespace OnlineExamSystem.Services.Services;
public class SetupService : ISetupService
{
    readonly ISetupRepository _setupRepository;
    readonly RoleManager<IdentityRole> _roleManager;

    public SetupService(ISetupRepository setupRepository, RoleManager<IdentityRole> roleManager)
    {
        _setupRepository = setupRepository;
        _roleManager = roleManager;
    }

    public async Task<BaseResponse> CreateRole(AddRoleRequest newRole)
    {
        var validator = new AddRoleValidator(_roleManager);
        var validationResult = await validator.ValidateAsync(newRole);
        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var result = _setupRepository.CreateRole(newRole);
        if (result.Result.Succeeded)
        {
            return new BaseResponse(Guid.NewGuid().ToString(), "New role is added successfully.", null, true);
        }
        return new BaseResponse(Guid.NewGuid().ToString(), "Failed to add new role!", result.Result.Errors.Select(a => a.Description).ToList(), false);
    }

    public List<IdentityRole> GetAllRoles()
    {
        return _setupRepository.GetAllRoles();
    }
}
