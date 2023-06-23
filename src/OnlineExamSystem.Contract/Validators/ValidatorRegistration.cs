using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineExamSystem.Common.Dtos;

namespace OnlineExamSystem.Common.Validators;
public static class ValidatorRegistration
{
    public static IServiceCollection AddValidatorServices(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AddRoleRequest>, AddRoleValidator>();
        return services;
    }
}
