using Microsoft.Extensions.DependencyInjection;
using OnlineExamSystem.Contract.Abstract;
using OnlineExamSystem.DAL.Repositories;

namespace OnlineExamSystem.DAL;
public static class RepositoryConfiguration
{
    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISetupRepository, SetupRepository>();
        return services;
    }
}
