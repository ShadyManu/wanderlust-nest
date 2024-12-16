using Application.Commons.Interfaces.Services;
using Application.Dto.AIResponse;
using Carter.OpenApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Extensions;

namespace Presentation.Endpoints;

public class ChatAiEndpoints() : ApiModule("/chat-ai")
{
    private const string EndpointTag = "Chat-AI";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (IAgentAIService agentService, ChatAiRequest request) => 
                await agentService.GetNormalResponseAsync(request.UserMessage, request.TravelDetails))
            .RequireAuthorization()
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
        
        app.MapPost("/add-to-itinerary", async (IAgentAIService agentService, string travelResponse) => 
                await agentService.AddToItineraryAsync(travelResponse))
            .RequireAuthorization()
            .IncludeInOpenApi()
            .WithTags(EndpointTag);
    }
}