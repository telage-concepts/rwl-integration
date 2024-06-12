using Core.EdTech.Domain.DataAccess.Implementations;
using Core.EdTech.Domain.DataAccess.Interfaces;
using Core.EdTech.Infrastructure.Services.Abstracts;
using Core.EdTech.Infrastructure.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Core.EdTech.Infrastructure.DI
{
  public static class CoreServicesDI
    {
        public static IServiceCollection AddCoreServicesDI(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IRWLService, RWLService>();

            return services;
        }

    }
}
