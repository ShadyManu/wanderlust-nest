using CCTemplate.Domain.Common.Interfaces;

namespace CCTemplate.Domain.Entities;

public class TodoItemEntity : IBaseAuditableEntity
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Note { get; set; }
    public required int Priority { get; set; }
    public DateTime? Reminder { get; set; }
    public DateTimeOffset Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
