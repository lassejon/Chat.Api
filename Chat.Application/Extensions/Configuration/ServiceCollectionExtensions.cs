using Chat.Application.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Application.Extensions.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSettings<T>(this IServiceCollection services) where T : Settings<T>
    {
        var settings = Activator.CreateInstance<T>();
        
        services = settings.OnConfigure(services);

        return services;
    }
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}