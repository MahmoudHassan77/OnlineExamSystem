using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Common.Validators;
public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    readonly UserManager<ApplicationUser> _userManager;

    public CreateUserValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        RuleFor(a => a.Email)
            .NotEmpty()
            .WithMessage("Email is requird.")
            .EmailAddress()
            .WithMessage("Email is not valid.")
            .MustAsync(async(email, token) =>
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(email);
                return user == null;
            })
            .WithMessage("Email is already existed.");
        RuleFor(a => a.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MaximumLength(24)
            .WithMessage("Password maximum length is 24.")
            .MinimumLength(8)
            .WithMessage("Password minimum length is 8.");
        RuleFor(a => a.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .MaximumLength(20)
            .WithMessage("Username maximum length is 20.")
            .MinimumLength(5)
            .WithMessage("Username minimum length is 4.")
            .MustAsync(async(username, token) =>
            {
                ApplicationUser? user = await _userManager.FindByNameAsync(username.ToLower());
                return user == null;
            })
            .WithMessage("Username is already existed.");
        RuleFor(a => a.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .MustAsync(async (phoneNumber, token) =>
            {

                ApplicationUser? user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
                return user == null;
            })
            .WithMessage("Phone number is already existed.");


    }
}
