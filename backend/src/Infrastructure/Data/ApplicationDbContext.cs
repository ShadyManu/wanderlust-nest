using System.Reflection;
using CCTemplate.Application.Common.Interfaces;
using CCTemplate.Domain.Entities;
using CCTemplate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CCTemplate.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoItemEntity> TodoItems => Set<TodoItemEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Every entity that inherits from IEntityTypeConfiguration will be configured
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
