using Domain.Interfaces;

namespace Domain.Entities.Common;

public class AuditableEntity : IAuditableEntity, IBaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
    public required Guid CreatedBy { get; set; }
    public DateTimeOffset? LastModified { get; set; } = DateTimeOffset.UtcNow;
    public required Guid LastModifiedBy { get; set; }
}