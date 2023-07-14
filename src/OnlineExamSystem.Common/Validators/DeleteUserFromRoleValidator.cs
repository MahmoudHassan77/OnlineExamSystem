using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Common.Validators;
public class DeleteUserFromRoleValidator : AbstractValidator<DeleteUserfromRoleDto>
{
    readonly UserManager<ApplicationUser> _userManager;
    readonly RoleManager<IdentityRole> _roleManager;

    public DeleteUserFromRoleValidator(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        RuleFor(e => e.RoleName)
           .NotEmpty()
           .NotNull()
           .WithMessage("Role name is required.")
           .MustAsync(async (role, token) =>
           {
               var roleExist = await _roleManager.RoleExistsAsync(role);
               return roleExist;
           })
           .WithMessage("{PropertyName} doesn't exist.");
        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .MustAsync(async (email, token) => await CheckUserExists(email))
            .WithMessage("{PropertyName} doesn't exist.")
            .MustAsync(async (x, email, token) =>
            {
                var user = await _userManager.FindByEmailAsync(email);
                return await _userManager.IsInRoleAsync(user, x.RoleName);
            }).WhenAsync(async (x, context, token) =>
            {
                return await CheckUserExists(x.Email);
            }, ApplyConditionTo.CurrentValidator)
            .WithMessage("User is not assigned to this role.");
    }
    private async Task<bool> CheckUserExists(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }
}
