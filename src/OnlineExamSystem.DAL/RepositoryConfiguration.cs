using Microsoft.Extensions.DependencyInjection;
using OnlineExamSystem.Common.Contracts.Repositories;
using OnlineExamSystem.DAL.Repositories;

namespace OnlineExamSystem.DAL;
public static class RepositoryConfiguration
{
    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISetupRepository, SetupRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        return services;
    }
}
