using Domain.Entities.Common;
using Domain.Interfaces;

namespace Domain.Entities.Planner;

public class TripEntity : AuditableEntity, IStartEndDate
{
    public virtual string Name => string.Join("-", Places.Select(e => e.City?.Name));
    
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    public virtual List<PlaceEntity> Places { get; init; } = [];
}