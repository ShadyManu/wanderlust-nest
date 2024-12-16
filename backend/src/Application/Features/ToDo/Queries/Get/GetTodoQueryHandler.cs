using Application.Commons.Interfaces;
using Application.Commons.Interfaces.Repositories;
using Application.Commons.Result;
using Application.Dto.Todo;
using Mapster;

namespace Application.Features.ToDo.Queries.Get;

public record GetTodoQuery(Guid Id) : IQuery<TodoDto>;

internal sealed class GetTodoQueryHandler(ITodoRepository repository) : IQueryHandler<GetTodoQuery, TodoDto>
{
    public async Task<Result<TodoDto>> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return Result<TodoDto>.Failure("Entity not found");
        }
        
        return Result<TodoDto>.Success(entity.Adapt<TodoDto>());
    }
}