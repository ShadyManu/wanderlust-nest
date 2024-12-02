using Application.Features.Todo.Queries.GetAllTodos;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints.Todo;

public class TodoEndpoints : CarterModule
{
    protected readonly ISender _sender;
    public TodoEndpoints(ISender sender)
        : base("/todo")
    {
        _sender = sender;
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async () => await _sender.Send(new GetAllTodosQuery()));
    }
}
