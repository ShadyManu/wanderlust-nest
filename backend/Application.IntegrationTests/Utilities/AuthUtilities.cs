using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Utilities;

public static class AuthUtilities
{
    private const string Email = "test@test.com";
    private const string Password = "TestUser_2024!@";
    
    
    public static async Task SeedTestUserAsync(IntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();

        var httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        var registerRequest = new
        {
            Email = Email,
            Password = Password,
            ConfirmPassword = Password
        };

        var response = await httpClient.PostAsJsonAsync("/register", registerRequest);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Registration failed. Status: {response.StatusCode}, Response: {errorContent}");
        }
    }

    public static async Task<string> GetTokenAsync(HttpClient httpClient)
    {
        var loginResponse = await httpClient.PostAsJsonAsync("/login", new
        {
            Email = Email,
            Password = Password
        });
        var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<AccessTokenResponse>();
        
        return tokenResponse?.AccessToken ?? string.Empty;
    }
}