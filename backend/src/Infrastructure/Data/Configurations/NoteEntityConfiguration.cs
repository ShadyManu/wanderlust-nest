using Domain.Entities;
using Infrastructure.Data.Configurations.Common;
using Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class NoteEntityConfiguration : BaseEntityConfiguration<NoteEntity>
{
    public override void Configure(EntityTypeBuilder<NoteEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable(TableNames.Notes);

        builder.Property(e => e.Text)
            .IsRequired(true);
        
        builder.Property(e => e.IsFavourite)
            .IsRequired(true)
            .HasDefaultValue(false);
    }
}