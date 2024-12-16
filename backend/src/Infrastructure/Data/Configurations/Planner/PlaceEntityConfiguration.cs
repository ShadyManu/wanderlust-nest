using Domain.Entities.Planner;
using Infrastructure.Data.Configurations.Common;
using Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Planner;

public class PlaceEntityConfiguration : AuditableEntityConfiguration<PlaceEntity>
{
    public override void Configure(EntityTypeBuilder<PlaceEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable(TableNames.Places);

        builder.Property(e => e.Address)
            .IsRequired();
        builder.Property(e => e.StartDate)
            .IsRequired();
        builder.Property(e => e.EndDate)
            .IsRequired();
        
        builder.Property(e => e.TripId)
            .IsRequired();
        
        builder.Property(e => e.CityId)
            .IsRequired();
        builder.HasOne(e => e.City)
            .WithMany()
            .HasForeignKey(e => e.CityId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(e => e.Activities)
            .WithOne()
            .HasForeignKey(e => e.PlaceId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}