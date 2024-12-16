using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Common;

public class AuditableEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity>
    where TEntity : class, IAuditableEntity, IBaseEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(e => e.Created)
            .IsRequired();
        
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(e => e.LastModified)
            .IsRequired();
        
        builder.Property(e => e.LastModifiedBy)
            .HasMaxLength(50)
            .IsRequired();
    }
}