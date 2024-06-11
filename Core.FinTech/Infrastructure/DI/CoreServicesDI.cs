using Core.FinTech.Domain.Contexts;
using Core.FinTech.Domain.DataAccess.Implementations;
using Core.FinTech.Domain.DataAccess.Interfaces;
using Core.FinTech.Infrastructure.Services.Abstracts;
using Core.FinTech.Infrastructure.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Core.FinTech.Infrastructure.DI
{
  public static class CoreServicesDI
    {
        public static IServiceCollection AddCoreServicesDI(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IRWLService, RWLService>();
            return services;
        }

    }
}
