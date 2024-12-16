using Domain.Entities.Planner;
using Domain.Enums;
using Infrastructure.Data.Configurations.Common;
using Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Planner;

public class ActivityEntityConfiguration : AuditableEntityConfiguration<ActivityEntity>
{
    public override void Configure(EntityTypeBuilder<ActivityEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable(TableNames.Activities);

        builder.Property(e => e.Name)
            .IsRequired();
        builder.Property(e => e.Description)
            .IsRequired(false);
        builder.Property(e => e.AdditionalNotes)
            .IsRequired(false);
        builder.Property(e => e.Recommendations)
            .IsRequired(false);
        builder.Property(e => e.StartDate)
            .IsRequired();
        builder.Property(e => e.EndDate)
            .IsRequired();
        builder.Property(e => e.ActivityType)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (ActivityTypeEnum)Enum.Parse(typeof(ActivityTypeEnum), v)
            );
        builder.Property(e => e.Link)
            .IsRequired(false);
        
        builder.Property(e => e.PlaceId)
            .IsRequired();
    }
}