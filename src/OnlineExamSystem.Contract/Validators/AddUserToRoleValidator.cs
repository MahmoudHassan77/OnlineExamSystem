using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Common.Validators;
class AddUserToRoleValidator : AbstractValidator<AddUserToRoleDto>
{
    readonly UserManager<ApplicationUser> _userManager;
    readonly RoleManager<IdentityRole> _roleManager;

    public AddUserToRoleValidator(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        // Check if user exist, if role exist, if user assigned to this role.
        RuleFor(e => e.RoleName)
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .MustAsync(async (role, token) =>
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                return roleExist;
            })
            .WithMessage("{PropertyName} doesn't exist.");
        RuleFor(e => e.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .MustAsync(async (email, token) =>
            {
                return await CheckUserExisted(email);
            })
            .WithMessage("{PropertyName} doesn't exist.")
            .MustAsync(async (x, email, token) =>
            {
                var user = await _userManager.FindByEmailAsync(email);
                return await _userManager.IsInRoleAsync(user, x.RoleName);
            })
            .WithMessage("User is already assigned to this role.")
            .WhenAsync(async (x, context, token) =>
            {
                return await CheckUserExisted(x.Email);
            }, ApplyConditionTo.CurrentValidator);


    }

    private async Task<bool> CheckUserExisted(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }
}
