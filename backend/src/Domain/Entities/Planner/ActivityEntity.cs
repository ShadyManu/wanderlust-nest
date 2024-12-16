using Domain.Entities.Common;
using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities.Planner;

public class ActivityEntity : AuditableEntity, IStartEndDate
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public string? AdditionalNotes { get; init; }
    public string? Recommendations { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
    public required ActivityTypeEnum ActivityType { get; init; }
    public string? Link { get; init; }
    
    // FK : Many to One
    public required Guid PlaceId { get; init; }
    
    public List<FileResourceEntity> Files { get; init; } = [];
}