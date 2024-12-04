using Domain.Interfaces;

namespace Domain.Entities.Common;

public class AuditableEntity : IAuditableEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset Created { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? LastModified { get; set; }
    public Guid? LastModifiedBy { get; set; }
}