using Application.Commons.Interfaces;
using Application.Commons.Interfaces.Repositories;
using Application.Commons.Result;
using Application.Dto.Todo;
using Mapster;

namespace Application.Features.Todo.Queries.GetAll;

public sealed record GetAllTodosQuery : IQuery<List<TodoDto>>;

internal sealed class GetAllTodosQueryHandler(ITodoRepository repository)
    : IQueryHandler<GetAllTodosQuery, List<TodoDto>>
{
    public async Task<Result<List<TodoDto>>> Handle(GetAllTodosQuery query, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(cancellationToken);
        return Result<List<TodoDto>>.Success(entities.Adapt<List<TodoDto>>());
    }
}
