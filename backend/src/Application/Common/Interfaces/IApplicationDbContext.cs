using CCTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CCTemplate.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoItemEntity> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
