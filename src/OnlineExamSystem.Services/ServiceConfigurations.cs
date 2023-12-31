﻿using Microsoft.Extensions.DependencyInjection;
using OnlineExamSystem.Common.Contracts.Services;
using OnlineExamSystem.Services.Services;

namespace OnlineExamSystem.Services;
public static class ServiceConfigurations
{
    public static IServiceCollection AddApplicationService(this IServiceCollection service)
    {
        service.AddScoped<ISetupService, SetupService>();
        service.AddTransient<IValidationService, ValidationService>();
        service.AddScoped<IAuthService, AuthService>();

        return service;
    }
}
