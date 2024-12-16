namespace Application.Dto.AIResponse;

public class ChatAiRequest
{
    public required string UserMessage { get; init; }
    public string? TravelDetails { get; init; }
}