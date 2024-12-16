using Domain.Entities;
using Domain.Entities.Planner;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User>(options)
{
    public virtual DbSet<TodoEntity> Todos { get; init; }
    public virtual DbSet<NoteEntity> Notes { get; init; }

    #region Planner

    public virtual DbSet<ActivityEntity> Activities { get; init; }
    public virtual DbSet<CityEntity> Cities { get; init; }
    public virtual DbSet<PlaceEntity> Places { get; init; }
    public virtual DbSet<TripEntity> Trips { get; init; }

    #endregion
    
    public virtual DbSet<FileResourceEntity> FileResources { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Every class that inherits from IEntityTypeConfiguration will be automatically configured
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        // Apply a different schema based on the prefix of the table
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (tableName == null) continue;

            entity.SetSchema(tableName.StartsWith("Asp") ? "identity" : "app");
        }
    }
}