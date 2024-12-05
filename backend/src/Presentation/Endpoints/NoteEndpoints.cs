using Application.Dto.Note;
using Application.Features.Note.Commands.Create;
using Application.Features.Note.Commands.Delete;
using Application.Features.Note.Commands.Update;
using Application.Features.Note.Queries.GetAll;
using Carter.OpenApi;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Extensions;

namespace Presentation.Endpoints;

public class NoteEndpoints() : ApiModule("/notes")
{
    private const string EndpointTag = "Notes";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (ISender sender) => 
                await sender.Send(new GetAllNotesQuery()))
            .RequireAuthorization()
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
        
        app.MapPost("/", async (ISender sender, CreateNoteDto note) =>
                await sender.Send(new CreateNoteCommand(note)))
            .RequireAuthorization()
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
        
        app.MapPatch("/", async (ISender sender, UpdateNoteDto updatedNote) => 
                await sender.Send(new UpdateNoteCommand(updatedNote)))
            .RequireAuthorization()
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
        
        app.MapDelete("/{id:guid}", async (ISender sender, Guid id) => 
                await sender.Send(new DeleteNoteCommand(id)))
            .RequireAuthorization()
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
    }
}