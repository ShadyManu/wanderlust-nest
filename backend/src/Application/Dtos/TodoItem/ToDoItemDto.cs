using AutoMapper;
using CCTemplate.Domain.Entities;

namespace CCTemplate.Application.Dtos.TodoItem;
public class TodoItemDto
{
    public Guid Id { get; init; }

    public required string Title { get; init; }

    public required int Priority { get; init; }

    public string? Note { get; init; }

    private class ToDoItemMapping : Profile
    {
        public ToDoItemMapping()
        {
            CreateMap<TodoItemEntity, TodoItemDto>().ForMember(d => d.Priority,
                opt => opt.MapFrom(s => s.Priority))
                .ReverseMap();
        }
    }
}
