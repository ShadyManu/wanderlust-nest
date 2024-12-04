using Application.Commons.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User>(options), IApplicationDbContext
{
    public virtual DbSet<TodoEntity> Todos { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Every class that inherits from IEntityTypeConfiguration will be automatically configured
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        // Applica uno schema diverso in base al prefisso del nome della tabella
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (tableName != null)
            {
                if (tableName.StartsWith("Asp"))
                {
                    entity.SetSchema("identity");
                }
                else
                {
                    entity.SetSchema("app");
                }
            }
        }
    }
}