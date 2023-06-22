using Microsoft.Extensions.DependencyInjection;
using OnlineExamSystem.Contract.Abstract;
using OnlineExamSystem.Services.Services;

namespace OnlineExamSystem.Services;
public static class ServiceConfigurations
{
    public static IServiceCollection AddApplicationService(this IServiceCollection service)
    {
        service.AddScoped<ISetupService, SetupService>();

        return service;
    }
}
