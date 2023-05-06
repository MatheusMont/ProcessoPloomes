using ApiPloomes.API.Configurations.Notifications;
using ApiPloomes.DOMAIN.Interfaces.INotifier;

namespace ApiPloomes.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
