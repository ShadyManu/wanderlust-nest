using Application.Commons.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructureDependencyInjection(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Database
        var databaseProvider = configuration["Database:DatabaseProvider"] ?? "PostgreSQL";

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        
        if (databaseProvider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(
                (sp, options) =>
                {
                    options.UseNpgsql(connectionString);
                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                });
        }
        else if (databaseProvider.Equals("InMemory", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<ApplicationDbContext>(
                (sp, options) =>
                {
                    options.UseInMemoryDatabase("InMemoryDb");
                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                    options.LogTo(Console.WriteLine);
                });
        }
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<ApplicationDbContextInitializer>();

        // Identity provider
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();
        services
            .AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
        
        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();
    }
}
