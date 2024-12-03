using Application.Commons.Interfaces;
using Application.Commons.Result;
using Domain.Entities;
using Infrastructure.Data;

namespace Application.Features.ToDo.Command.Create;

public sealed record CreateTodoCommand(string Title, string Description) : ICommand<bool>;

internal sealed class CreateTodoCommandHandler(ApplicationDbContext context) : ICommandHandler<CreateTodoCommand, bool>
{
    public async Task<Result<bool>> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var entity = new TodoEntity() { Title = command.Title, Description = command.Description };
        
        await context.AddAsync(entity, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Success(result > 0);
    }
}
