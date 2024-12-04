using Application.Dto.Note;
using Application.Dto.Todo;
using Domain.Entities;
using Mapster;

namespace Application.Dto;

public static class MapsterConfig
{
    public static void Configure()
    {
        // Configure TODO
        TypeAdapterConfig<TodoEntity, TodoDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description);
            // example of nested objects mapping
            //.Map(dest => dest.Reviews, src => src.Reviews.Adapt<List<ReviewDto>>())

        TypeAdapterConfig<TodoEntity, CreateTodoDto>.NewConfig();
        
        // Configure Note
        TypeAdapterConfig<NoteEntity, NoteDto>.NewConfig();
        TypeAdapterConfig<NoteEntity, CreateNoteDto>.NewConfig();
    }
}