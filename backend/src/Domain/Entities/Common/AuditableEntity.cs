using Domain.Interfaces;

namespace Domain.Entities.Common;

public class AuditableEntity : IAuditableEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}