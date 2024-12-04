using Domain.Entities.Common;

namespace Domain.Entities;

public class NoteEntity : AuditableEntity
{
    public required string Text { get; init; }
}