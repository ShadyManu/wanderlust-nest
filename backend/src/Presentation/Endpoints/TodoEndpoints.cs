using Application.Dto.Todo;
using Application.Features.ToDo.Commands.Create;
using Application.Features.ToDo.Commands.Delete;
using Application.Features.ToDo.Commands.Update;
using Application.Features.ToDo.Queries.Get;
using Application.Features.Todo.Queries.GetAll;
using Carter.OpenApi;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Extensions;

namespace Presentation.Endpoints;

public class TodoEndpoints() : ApiModule("/todo")
{
    private const string EndpointTag = "Todo";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        // GET
        app.MapGet("/", async (ISender sender) =>
                await sender.Send(new GetAllTodosQuery()))
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
        
        app.MapGet("/{id}", async (ISender sender, Guid id) => 
                await sender.Send(new GetTodoQuery(id)))
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
        
        
        // POST
        app.MapPost("/", async (ISender sender, CreateTodoDto createTodoDto) => 
                await sender.Send(new CreateTodoCommand(createTodoDto)))
            .IncludeInOpenApi()
            .WithTags(EndpointTag);

        
        // PATCH
        app.MapPatch("/", async (ISender sender, UpdateTodoDto updatedEntry) =>
                await sender.Send(new UpdateTodoCommand(updatedEntry)))
            .IncludeInOpenApi()
            .WithTags(EndpointTag);

        
        // DELETE
        app.MapDelete("/{id}", async (ISender sender, Guid id) =>
                await sender.Send(new DeleteTodoCommand(id)))
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
        
        app.MapDelete("/", async (ISender sender) => 
                await sender.Send(new DeleteAllTodosCommand()))
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
    }
}
