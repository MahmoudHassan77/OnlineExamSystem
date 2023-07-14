using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Common.Validators;
public class GetUserRolesValidator : AbstractValidator<GetUserRolesDto>
{
    readonly UserManager<ApplicationUser> _userManager;

    public GetUserRolesValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        RuleFor(a => a.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .MustAsync(async (email, token) =>
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user != null;
            })
            .WithMessage("User doesn't exist.");
    }
}
