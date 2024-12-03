using Application.Commons.Interfaces;
using Application.Commons.Result;
using Application.Dto.Todo;
using Infrastructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Todo.Queries.GetAll;

public sealed record GetAllTodosQuery : IQuery<List<TodoDto>>;

internal sealed class GetAllTodosQueryHandler(ApplicationDbContext context)
    : IQueryHandler<GetAllTodosQuery, List<TodoDto>>
{
    public async Task<Result<List<TodoDto>>> Handle(GetAllTodosQuery query, CancellationToken cancellationToken)
    {
        var entities = await context.Todos.ToListAsync(cancellationToken);
        return Result<List<TodoDto>>.Success(entities.Adapt<List<TodoDto>>());
    }
}
