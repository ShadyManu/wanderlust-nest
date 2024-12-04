using Application.Commons.Interfaces;
using Application.Commons.Result;
using Application.Dto.Todo;
using Domain.Entities;
using Mapster;

namespace Application.Features.ToDo.Commands.Create;

public sealed record CreateTodoCommand(CreateTodoDto CreateTodoDto) : ICommand<bool>;

internal sealed class CreateTodoCommandHandler(IApplicationDbContext context) : ICommandHandler<CreateTodoCommand, bool>
{
    public async Task<Result<bool>> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var entity = command.CreateTodoDto.Adapt<TodoEntity>();
        
        await context.Todos.AddAsync(entity, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Success(result > 0);
    }
}
