using Application.Dtos.Todo;
using MediatR;

namespace Application.Features.Todo.Queries.GetAllTodos;

public sealed record GetAllTodosQuery() : IRequest<List<TodoDto>>;
