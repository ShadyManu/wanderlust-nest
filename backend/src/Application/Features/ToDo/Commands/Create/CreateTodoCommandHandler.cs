using Application.Commons.Interfaces;
using Application.Commons.Interfaces.Repositories;
using Application.Commons.Result;
using Application.Dto.Todo;
using Domain.Entities;
using Mapster;

namespace Application.Features.ToDo.Commands.Create;

public sealed record CreateTodoCommand(CreateTodoDto CreateTodoDto) : ICommand<TodoDto>;

internal sealed class CreateTodoCommandHandler(ITodoRepository repository) : ICommandHandler<CreateTodoCommand, TodoDto>
{
    public async Task<Result<TodoDto>> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var entity = command.CreateTodoDto.Adapt<TodoEntity>();
        
        var result = await repository.AddAsync(entity, cancellationToken);

        return result == 0 
            ? Result<TodoDto>.Failure("Failed to create Todo.") 
            : Result<TodoDto>.Success(entity.Adapt<TodoDto>());
    }
}
