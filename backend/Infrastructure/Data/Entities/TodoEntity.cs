namespace Infrastructure.Data.Entities;

public class TodoEntity
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
