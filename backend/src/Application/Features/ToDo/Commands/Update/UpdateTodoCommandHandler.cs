using Application.Commons.Interfaces;
using Application.Commons.Interfaces.Repositories;
using Application.Commons.Result;
using Application.Dto.Todo;
using Mapster;

namespace Application.Features.ToDo.Commands.Update;

public record UpdateTodoCommand(UpdateTodoDto UpdatedEntry) : ICommand<TodoDto>;

internal sealed class UpdateTodoCommandHandler(ITodoRepository repository) : ICommandHandler<UpdateTodoCommand, TodoDto>
{
    public async Task<Result<TodoDto>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var oldEntity = await repository.GetByIdAsync(request.UpdatedEntry.Id, cancellationToken);
        if (oldEntity is null)
        {
            return Result<TodoDto>.Failure(new ResultError("Entity to update not found"));
        }
        
        request.UpdatedEntry.Adapt(oldEntity);
        var result = await repository.SaveChangesAsync(cancellationToken);
        
        return Result<TodoDto>.Success(oldEntity.Adapt<TodoDto>());
    }
}