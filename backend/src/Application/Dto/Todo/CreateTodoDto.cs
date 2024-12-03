namespace Application.Dto.Todo;

public class CreateTodoDto
{
    public string Title { get; init; } = null!;
    public string? Description { get; init; } = null;
}