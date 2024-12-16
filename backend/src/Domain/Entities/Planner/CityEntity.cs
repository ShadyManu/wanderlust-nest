using Domain.Entities.Common;

namespace Domain.Entities.Planner;

public class CityEntity : AuditableEntity
{
    public required string Name { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required string LocalCurrency { get; init; }
    public required string LocalCurrencySymbol { get; init; }
    public required string CountryFullName { get; init; }
    public required string CountryThreeLetters { get; init; }
    public string? FamousDishesPipe { get; init; }
    public virtual List<string> FamousDishes => FamousDishesPipe?.Split("|").ToList() ?? [];
}