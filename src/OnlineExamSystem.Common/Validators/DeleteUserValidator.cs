using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Common.Validators;
public class DeleteUserValidator : AbstractValidator<DeleteUserDto>
{
    readonly UserManager<ApplicationUser> _userManager;

    public DeleteUserValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

        RuleFor(u => u.Id)
            .NotEmpty()
            .WithMessage("User id is required.")
            .MustAsync(IsUserExist)
            .WithMessage("User with this id doesn't exist");
    }

    private async Task<bool> IsUserExist(string id, CancellationToken token)
    {
        var isExist = await _userManager.FindByIdAsync(id);
        return isExist != null;
    }
}
