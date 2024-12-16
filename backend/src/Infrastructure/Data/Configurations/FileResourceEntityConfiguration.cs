using Domain.Entities;
using Infrastructure.Data.Configurations.Common;
using Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class FileResourceEntityConfiguration: AuditableEntityConfiguration<FileResourceEntity>
{
    public override void Configure(EntityTypeBuilder<FileResourceEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable(TableNames.FileResources);

        builder.Property(e => e.EntityId)
            .IsRequired();
        builder.Property(e => e.EntityType)
            .IsRequired();
        builder.Property(e => e.FileName)
            .IsRequired();
        builder.Property(e => e.FileSize)
            .IsRequired();
        builder.Property(e => e.FileExtension)
            .IsRequired();
        builder.Property(e => e.FileType)
            .IsRequired();
        builder.Property(e => e.Url)
            .IsRequired();
    }
}