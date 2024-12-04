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
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
        
        app.MapPost("/", async (ISender sender, CreateNoteDto note) =>
                await sender.Send(new CreateNoteCommand(note)))
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
        
        app.MapPatch("/", async (ISender sender, UpdateNoteDto updatedNote) => 
                await sender.Send(new UpdateNoteCommand(updatedNote)))
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
        
        app.MapDelete("/", async (ISender sender, Guid noteId) => 
                await sender.Send(new DeleteNoteCommand(noteId)))
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
    }
}