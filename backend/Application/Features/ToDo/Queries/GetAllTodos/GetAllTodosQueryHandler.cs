using Application.Dtos.Todo;
using Infrastructure.Data.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Todo.Queries.GetAllTodos;

internal sealed class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<TodoDto>>
{
    // Mocked todos
    List<TodoEntity> todos =
    [
        new() { Id = Guid.NewGuid(), Title = "First Title", Description = "First Description" },
        new() { Id = Guid.NewGuid(), Title = "Second Title", Description = "Second Description" },
        new() { Id = Guid.NewGuid(), Title = "Third Title", Description = "Third Description" },
    ];

    async Task<List<TodoDto>> IRequestHandler<GetAllTodosQuery, List<TodoDto>>.Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        var todos = this.todos;

        return await Task.FromResult(todos.Adapt<List<TodoDto>>());
    }
}
