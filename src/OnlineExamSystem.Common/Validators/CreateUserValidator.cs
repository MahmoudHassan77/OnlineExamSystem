using FluentValidation;
using OnlineExamSystem.Common.Dtos;

namespace OnlineExamSystem.Common.Validators;
public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        
    }
}
