using Domain.Entities.Planner;
using Infrastructure.Data.Configurations.Common;
using Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Planner;

public class CityEntityConfiguration : AuditableEntityConfiguration<CityEntity>
{
    public override void Configure(EntityTypeBuilder<CityEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable(TableNames.Cities);

        builder.Property(e => e.Name)
            .IsRequired();
        builder.Property(e => e.Latitude)
            .IsRequired();
        builder.Property(e => e.Longitude)
            .IsRequired();
        builder.Property(e => e.LocalCurrency)
            .IsRequired();
        builder.Property(e => e.LocalCurrencySymbol)
            .IsRequired();
        builder.Property(e => e.CountryFullName)
            .IsRequired();
        builder.Property(e => e.CountryThreeLetters)
            .IsRequired();
        builder.Property(e => e.FamousDishes)
            .IsRequired(false);
        
        builder.HasIndex(c => new { c.Name, c.CountryThreeLetters })
            .IsUnique();

        builder.Ignore(e => e.FamousDishes);
    }
}