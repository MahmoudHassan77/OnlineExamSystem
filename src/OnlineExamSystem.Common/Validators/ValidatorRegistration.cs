using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineExamSystem.Common.Dtos;

namespace OnlineExamSystem.Common.Validators;
public static class ValidatorRegistration
{
    public static IServiceCollection AddValidatorServices(this IServiceCollection services)
    {
        services.AddFluentValidation(opt =>
        {
            opt.AutomaticValidationEnabled = false;
            opt.RegisterValidatorsFromAssembly(typeof(AddRoleValidator).Assembly);
        });
        return services;
    }
}
