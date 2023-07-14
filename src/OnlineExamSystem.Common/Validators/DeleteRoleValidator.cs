using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace OnlineExamSystem.Common.Validators;
public class DeleteRoleValidator : AbstractValidator<string>
{
    readonly RoleManager<IdentityRole> _roleManager;

    public DeleteRoleValidator(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        RuleFor(x => x)
            .NotNull()
            .NotEmpty()
            .WithMessage("Role ID is required.")
            .MustAsync(async (id, token) =>
            {
                var role = await _roleManager.FindByIdAsync(id);
                return role != null;
            })
            .WithMessage("Role doesn't exist.");
        
    }
}
