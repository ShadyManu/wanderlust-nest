using Application.Commons.Interfaces;
using Application.Commons.Result;
using Infrastructure.Data;

namespace Application.Features.ToDo.Command.Delete;

public record DeleteTodoCommand(Guid Id) : ICommand<bool>;

internal sealed class DeleteTodoCommandHandler(ApplicationDbContext context) : ICommandHandler<DeleteTodoCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
    {
        var entityToRemove = await context.Todos.FindAsync(command.Id, cancellationToken);
        if (entityToRemove is null)
        {
            return Result<bool>.Failure(new Exception("Not found"));
        }
        
        context.Todos.Remove(entityToRemove);
        var result = await context.SaveChangesAsync(cancellationToken);
        
        return Result<bool>.Success(result > 0);
    }
}