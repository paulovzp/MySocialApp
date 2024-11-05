using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySocialApp.Domain;
using MySocialApp.Infrastructure;
using MySocialApp.Persistence;

namespace MySocialApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPostAppService, PostAppService>();
        services.AddScoped<IUserAppService, UserAppService>();

        services.AddDomain();
        services.AddPersistence(configuration);
        services.AddInfrastructure(configuration);

        return services;
    }
}