using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public static class InitializerExtensions
{
    public static async Task MigrateAsync(this WebApplication app, bool seedData = false)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetService<ApplicationDbContextInitializer>();
        if (initializer is null) return;
        
        await initializer.ApplyMigrationsAsync();
    }

    public static async Task SeedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetService<ApplicationDbContextInitializer>();
        if (initializer is null) return;
        
        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context)
{
    public async Task ApplyMigrationsAsync()
    {
        if (context.Database.IsRelational())
        {
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrate the database.");
                throw;
            }
        }
        else
        {
            Console.WriteLine("Skipping migrations for InMemoryDatabase.");
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seed the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        await Task.Delay(500);
        // TODO
    }
}