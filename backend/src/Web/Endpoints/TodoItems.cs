using CCTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;
using CCTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;
using CCTemplate.Application.Features.TodoItems.Commands.DeleteTodoItem;
using CCTemplate.Application.Features.TodoItems.Queries.Get;
using CCTemplate.Web.Infrastructure;
using MediatR;
using CCTemplate.Application.Dtos.TodoItem;

namespace CCTemplate.Web.Endpoints;

public class TodoItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateTodoItem)
            .MapPut(UpdateTodoItem, "{id}")
            .MapDelete(DeleteTodoItem, "{id}")
            .MapGet(GetTodoItems);
    }

    public Task<TodoItemDto> CreateTodoItem(ISender sender, CreateTodoItemCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateTodoItem(ISender sender, Guid id, UpdateTodoItemCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteTodoItem(ISender sender, int id)
    {
        await sender.Send(new DeleteTodoItemCommand(id));
        return Results.NoContent();
    }

    public async Task<List<TodoItemDto>> GetTodoItems(ISender sender)
    {
        var result = await sender.Send(new GetToDoItemsQuery());
        return result;
    }
}
