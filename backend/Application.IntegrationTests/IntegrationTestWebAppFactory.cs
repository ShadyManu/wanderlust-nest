using Application.IntegrationTests.Utilities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Application.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15")
        .WithDatabase("wanderlust-nest")
        .WithUsername("postgres")
        .WithPassword("postgres")
        // .WithPortBinding("5432", "5432")
        .Build();
        
    private static bool _started = false;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(e => e.ServiceType == typeof(ApplicationDbContext));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }
        
            var connectionString = _dbContainer.GetConnectionString();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await AuthUtilities.SeedTestUserAsync(this);
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}