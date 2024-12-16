using Domain.Entities.Common;
using Domain.Interfaces;

namespace Domain.Entities.Planner;

public class PlaceEntity : AuditableEntity, IStartEndDate
{
    public required string Address { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }

    public required Guid TripId { get; init; }

    public Guid CityId { get; init; }
    public virtual CityEntity? City { get; init; }
    
    public virtual List<ActivityEntity> Activities { get; init; } = [];
}