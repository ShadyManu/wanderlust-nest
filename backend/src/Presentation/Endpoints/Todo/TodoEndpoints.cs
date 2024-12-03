using Application.Dto.Todo;
using Application.Features.ToDo.Command.Create;
using Application.Features.ToDo.Command.Delete;
using Application.Features.Todo.Queries.GetAll;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Endpoints.Todo;

public class TodoEndpoints : CarterModule
{
    public TodoEndpoints(ISender sender, IServiceScopeFactory serviceScopeFactory)
        : base("/todo")
    { }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (ISender sender) => await sender.Send(new GetAllTodosQuery()));
        
        app.MapPost("/", async (ISender sender, CreateTodoDto command) 
            => await sender.Send(new CreateTodoCommand(command.Title, command.Description)));

        app.MapDelete("/", async (ISender sender, Guid id) =>
            await sender.Send(new DeleteTodoCommand(id)));
    }
}
