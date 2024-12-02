namespace CCTemplate.Application.Dtos.TodoItem;
public class CreateTodoItemDto
{
    public required string Title { get; set; }
    public required int Priority { get; set; }
    public string? Note { get; set; }
}
