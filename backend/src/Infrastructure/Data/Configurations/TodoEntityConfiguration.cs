using Domain.Entities;
using Infrastructure.Data.Configurations.Common;
using Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TodoEntityConfiguration : BaseEntityConfiguration<TodoEntity>
{
    public override void Configure(EntityTypeBuilder<TodoEntity> builder)
    {
        base.Configure(builder);
        
        builder.ToTable(TableNames.Todos);
    }
}