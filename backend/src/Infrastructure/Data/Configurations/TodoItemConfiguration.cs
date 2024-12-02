using CCTemplate.Domain.Entities;
using CCTemplate.Infrastructure.Data.Configurations.Common;
using CCTemplate.Infrastructure.Data.TableNames;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCTemplate.Infrastructure.Data.Configurations;

public class TodoItemConfiguration : BaseAuditableEntityConfiguration<TodoItemEntity>, IEntityTypeConfiguration<TodoItemEntity>
{
    public override void Configure(EntityTypeBuilder<TodoItemEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable(DatabaseConstants.TodoItemTable);

        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
    }
}
