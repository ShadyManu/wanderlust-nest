using Application.Commons.Interfaces;
using Application.Commons.Result;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ToDo.Commands.Delete;

public record DeleteAllTodosCommand : ICommand<bool>;

internal sealed class DeleteAllTodosCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteAllTodosCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteAllTodosCommand request, CancellationToken cancellationToken)
    {
        var entitiesToRemove = await context.Todos.ToListAsync(cancellationToken);
        context.Todos.RemoveRange(entitiesToRemove);
        var result = await context.SaveChangesAsync(cancellationToken);
        
        return Result<bool>.Success(true);
    }
}