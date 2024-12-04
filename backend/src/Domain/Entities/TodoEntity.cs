using Domain.Entities.Common;

namespace Domain.Entities;

public class TodoEntity : AuditableEntity
{
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
}
