using Application.Commons.Interfaces;
using Application.Commons.Interfaces.Repositories;
using Application.Commons.Result;

namespace Application.Features.ToDo.Commands.Delete;

public record DeleteTodoCommand(Guid Id) : ICommand<bool>;

internal sealed class DeleteTodoCommandHandler(ITodoRepository repository) : ICommandHandler<DeleteTodoCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
    {
        var isDeleted = await repository.DeleteAsync(command.Id, cancellationToken);
        if (isDeleted == 0)
        {
            return Result<bool>.Failure(new ResultError("Entity to delete not found"));
        }
        
        return Result<bool>.Success(true);
    }
}