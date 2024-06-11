using Core.FinTech.Infrastructure.Helpers.AppSettings;
using Microsoft.Extensions.DependencyInjection;

namespace Core.FinTech.Infrastructure.DI
{
    public static class InfraSetupDI
    {
        public static IServiceCollection ConfigHelper(this IServiceCollection services, Action<ConfigProviders> options)
        {
            services.AddHttpContextAccessor();
            services.Configure(options);
            services.AddSingleton<ConfigOptions>();

            return services;
        }
    }
}
