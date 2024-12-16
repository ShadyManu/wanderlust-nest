using Domain.Entities.Common;

namespace Domain.Entities;

public class ApiKeyEntity : AuditableEntity
{
    public string? OpenAi { get; init; }
    public string? GoogleMaps { get; init; }
}