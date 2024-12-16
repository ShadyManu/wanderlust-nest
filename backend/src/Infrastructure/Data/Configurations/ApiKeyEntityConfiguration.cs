using Domain.Entities;
using Infrastructure.Data.Configurations.Common;
using Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ApiKeyEntityConfiguration : AuditableEntityConfiguration<ApiKeyEntity>
{
    public override void Configure(EntityTypeBuilder<ApiKeyEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable(TableNames.ApiKeys);

        builder.Property(e => e.OpenAi)
            .IsRequired(false);
        
        builder.Property(e => e.GoogleMaps)
            .IsRequired(false);
    }
}