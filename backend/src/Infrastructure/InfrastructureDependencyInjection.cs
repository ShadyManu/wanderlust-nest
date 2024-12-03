using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructureDependencyInjection(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:DefaultConnection"];
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(connectionString),
            ServiceLifetime.Scoped);
    }
}