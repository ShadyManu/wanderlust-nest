using Domain.Entities.Common;

namespace Domain.Entities;

public class FileResourceEntity : AuditableEntity
{
    public required Guid EntityId { get; init; }
    public string? EntityType { get; init; }
    public required string FileName { get; init; }
    public required long FileSize { get; init; }
    public required string FileExtension { get; init; }
    public required string FileType { get; init; }
    public required string Url { get; init; }
}