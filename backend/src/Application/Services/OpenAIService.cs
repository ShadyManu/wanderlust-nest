using System.Net.Http.Json;
using Application.Commons.Interfaces.Services;
using Application.Dto.AI;
using Newtonsoft.Json;

namespace Application.Services;

public class OpenAIService(HttpClient httpClient) : IAgentAIService
{
    private const string ApiKey = "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";

    public async Task<string> AddToItineraryAsync(string travelResponse)
    {
        var travelResponseCleaned = travelResponse.Replace("\\n", "").Replace("\\\"", "\"");
        var typedTravelResponse = JsonConvert.DeserializeObject<AITravelResponse>(travelResponseCleaned);
        if (typedTravelResponse is null)
        {
            return "Could not parse travel response";
        }
        return await Task.FromResult($"{typedTravelResponse.Name} Added to itinerary");
    }
    
    public async Task<string> GetNormalResponseAsync(string userMessage, string? travelDetails)
    {
        // Impostiamo l'header per l'API Key
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiKey);

        // TODO
        // if (string.IsNullOrEmpty(travelDetails))
        // {
        //     travelDetails = "User's trip is from December 19 to December 28. From December 19 to 24, they will be in Budapest, and from December 24 to 28, they will be in Prague.";
        // }
        
        // Messaggio per configurare il comportamento dell'AI
        var systemMessage = """
        You are a travel assistant. Your role is to help users find activities and suggestions for their trips. You should provide general suggestions based on the user's current travel plan, and never suggest things unrelated to their trip.

        The user is currently planning a trip, and you have the following details about it:
        """ + travelDetails + """

        When the user asks for suggestions on what to do, provide them with generic activity suggestions (e.g., 'visit a museum', 'go to a restaurant', etc.) without any structured data.

        If the user says something like 'Add this to my itinerary' or 'Put this on my schedule', respond with a structured JSON in the following format:
        {
            \"ActionType\": \"Create\",
            \"ActivityType\": \"Restaurant | Museum | Park | Shopping | Theatre | ...\",
            \"Name\": \"string\",
            \"Description\": \"string\",
            \"Location\": \"string\",
            \"Date\": \"YYYY-MM-DDTHH:MM:SS\",
            \"Duration\": \"HH:MM:SS\",
            \"Latitude\": double,
            \"Longitude\": double,
            \"Rating\": double,
            \"Tags\": [\"string\", ...],
            \"Images\": [\"string\", ...],
            \"Notes\": \"string\",
            \"Price\": decimal
        }
        Make sure the response is always in this exact JSON structure. If you are unsure, return a message asking for more details.
    """;
        
        // Costruisci la richiesta al modello
        var request = new
        {
            model = "gpt-3.5-turbo", // Modifica il modello se necessario
            messages = new[]
            {
                new { role = "system", content = systemMessage },
                new { role = "user", content = userMessage }
            }
        };

        // Invia la richiesta a OpenAI
        var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", request);
        response.EnsureSuccessStatusCode();

        // Leggi la risposta
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Se la risposta è JSON strutturata (per l'aggiunta all'itinerario), restituiscila
        return jsonResponse;
    } 
}