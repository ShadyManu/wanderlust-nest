using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commons.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoEntity> Todos { get; }
    DbSet<NoteEntity> Notes { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}