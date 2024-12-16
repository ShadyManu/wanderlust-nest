using Domain.Entities.Planner;
using Infrastructure.Data.Configurations.Common;
using Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Planner;

public class TripEntityConfiguration : AuditableEntityConfiguration<TripEntity>
{
    public override void Configure(EntityTypeBuilder<TripEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable(TableNames.Trips);

        builder.Property(e => e.StartDate)
            .IsRequired();
        builder.Property(e => e.EndDate)
            .IsRequired();
        
        builder.HasMany(e => e.Places)
            .WithOne()
            .HasForeignKey(e => e.TripId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}