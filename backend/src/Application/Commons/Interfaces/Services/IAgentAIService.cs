using Application.Dto.AIResponse;

namespace Application.Commons.Interfaces.Services;

public interface IAgentAIService
{
    Task<string> GetNormalResponseAsync(string userMessage, string? travelDetails);
    Task<string> AddToItineraryAsync(string travelResponse);
}