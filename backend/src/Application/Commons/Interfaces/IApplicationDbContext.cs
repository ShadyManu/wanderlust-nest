using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commons.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoEntity> Todos { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}