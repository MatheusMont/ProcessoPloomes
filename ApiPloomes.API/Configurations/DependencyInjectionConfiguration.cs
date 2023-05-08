using ApiPloomes.API.Configurations.Notifications;
using ApiPloomes.DATA.Repositories;
using ApiPloomes.DOMAIN.Interfaces.INotifier;
using ApiPloomes.DOMAIN.Interfaces.IRepository;
using ApiPloomes.DOMAIN.Interfaces.IServices;
using ApiPloomes.DOMAIN.Services;
using AutoMapper;

namespace ApiPloomes.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserServices, UserServices>();

            return services;
        }
    }
}
