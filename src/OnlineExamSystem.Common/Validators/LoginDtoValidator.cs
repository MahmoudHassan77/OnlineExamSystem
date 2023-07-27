using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Common.Validators;
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    readonly UserManager<ApplicationUser> _userManager;

    public LoginDtoValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        RuleFor(r=>r.Email)
            .NotEmpty()
            .WithMessage("Email is required.");
        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
        RuleFor(r => new { r.Email, r.Password })
            .MustAsync(async (x, token) =>
            {
                var user = await _userManager.FindByEmailAsync(x.Email);
                if (user == null) return false;
                bool isUser = await _userManager.CheckPasswordAsync(user, x.Password);
                return isUser;
            })
            .WithMessage("Email or password is incorrect.");

    }
}
