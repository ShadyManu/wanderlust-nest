using Domain.Enums;

namespace Application.Dto.AI;

public class AITravelResponse
{
    public AITravelActionType ActionType { get; init; }
    public ActivityTypeEnum ActivityType { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Location { get; init; }
    public DateTime? Date { get; init; }
    public TimeSpan? Duration { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public double? Rating { get; init; }
    public List<string> Tags { get; init; } = [];
    public List<string> Images { get; init; } = [];
    public string? Notes { get; init; }
    public decimal? Price { get; init; }
}

public enum AITravelActionType
{
    Create = 0,
    Delete = 1,
    Update = 2,
}

