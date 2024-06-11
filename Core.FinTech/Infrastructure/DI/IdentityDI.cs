using Core.FinTech.Domain.Contexts;
using Core.FinTech.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Core.FinTech.Infrastructure.DI
{
  public static class IdentityDI
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {

            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.Lockout.MaxFailedAccessAttempts = 3;
                opts.SignIn.RequireConfirmedAccount = false;

                opts.User.RequireUniqueEmail = true;
                opts.Password.RequireDigit = false;
                opts.Password.RequiredUniqueChars = 0;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;

                opts.ClaimsIdentity.UserNameClaimType = Claims.Name;
                opts.ClaimsIdentity.UserIdClaimType = Claims.Subject;
                opts.ClaimsIdentity.RoleClaimType = Claims.Role;

            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));
            return services;
        }
    }
}
