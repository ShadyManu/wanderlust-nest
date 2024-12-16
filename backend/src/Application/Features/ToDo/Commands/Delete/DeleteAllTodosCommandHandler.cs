using Application.Commons.Interfaces;
using Application.Commons.Interfaces.Repositories;
using Application.Commons.Result;

namespace Application.Features.ToDo.Commands.Delete;

public record DeleteAllTodosCommand : ICommand<bool>;

internal sealed class DeleteAllTodosCommandHandler(ITodoRepository repository) : ICommandHandler<DeleteAllTodosCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteAllTodosCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteAllAsync(cancellationToken);
        
        return result > 0 
            ? Result<bool>.Success(true)
            : Result<bool>.Failure("Failed to delete all");
    }
}