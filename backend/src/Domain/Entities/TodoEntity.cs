namespace Domain.Entities;

public class TodoEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
}
