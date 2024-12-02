using CCTemplate.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCTemplate.Infrastructure.Data.Configurations.Common;
public class BaseAuditableEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity>, IEntityTypeConfiguration<TEntity>
    where TEntity : class, IBaseAuditableEntity
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
