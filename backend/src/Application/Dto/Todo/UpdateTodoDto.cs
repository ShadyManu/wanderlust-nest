namespace Application.Dto.Todo;

public class UpdateTodoDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}