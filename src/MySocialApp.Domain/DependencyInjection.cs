using Microsoft.Extensions.DependencyInjection;

namespace MySocialApp.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {

        services.AddScoped<IPostService, PostService>();

        return services;
    }
}