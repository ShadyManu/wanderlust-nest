using Application.Commons.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUser user)
    : IdentityDbContext<User>(options), IApplicationDbContext
{
    public virtual DbSet<TodoEntity> Todos { get; init; }
    public virtual DbSet<NoteEntity> Notes { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Every class that inherits from IEntityTypeConfiguration will be automatically configured
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<NoteEntity>()
            .HasQueryFilter(e => e.CreatedBy == user.Id);
        
        // Apply identity schema for Identity tables, and App schema for all the others
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (tableName is null) continue;

            entity.SetSchema(tableName.StartsWith("Asp") ? "identity" : "app");
        }
    }
}