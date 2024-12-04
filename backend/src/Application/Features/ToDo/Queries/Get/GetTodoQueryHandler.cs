using Application.Commons.Interfaces;
using Application.Commons.Result;
using Application.Dto.Todo;
using Mapster;

namespace Application.Features.ToDo.Queries.Get;

public record GetTodoQuery(Guid Id) : IQuery<TodoDto>;

internal sealed class GetTodoQueryHandler(IApplicationDbContext context) : IQueryHandler<GetTodoQuery, TodoDto>
{
    public async Task<Result<TodoDto>> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Todos.FindAsync(request.Id, cancellationToken);
        
        return Result<TodoDto>.Success(entity.Adapt<TodoDto>());
    }
}